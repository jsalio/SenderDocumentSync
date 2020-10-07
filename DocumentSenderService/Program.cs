using DocumentSender;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.ServiceProcess;

namespace DocumentSenderService
{
    static class Program
    {
        internal const int SleepIntervalOnMilliseconds = 10_000;
        private static Service1 _service;
        private static ApplicationSocket _applicationSocket;
        private static Dictionary<ApplicationCommand, Func<string[], ApplicationWebCommandResponse<ApplicationStatus>>> _applicationCommands;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            int settingPort = ValidatePortConfig();
            Console.WriteLine($"Creating Socket on port {settingPort}");
            _applicationCommands = new Dictionary<ApplicationCommand, Func<string[], ApplicationWebCommandResponse<ApplicationStatus>>>
            {
                {ApplicationCommand.Stop, StopConnection},
                {ApplicationCommand.Send, SendDocument}
            };

            _applicationSocket = new ApplicationSocket(settingPort, OnAccept);
            if (Environment.UserInteractive)
            {
                using (var myService = new Service1())
                {
                    _service = myService;
                    _service.OnDebug();
                    Console.ReadLine();
                    _service.GetInstance().Disconect();
                    Console.WriteLine("Close connection");
                }
            }
            else
            {
                _service = new Service1();
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                   _service
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static ApplicationWebCommandResponse<ApplicationStatus> SendDocument(string[] arg)
        {
            try
            {
                ApplicationWebCommandResponse<ApplicationStatus> response = _service.SendDocument(long.Parse(arg[0]));
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApplicationWebCommandResponse<ApplicationStatus>()
                {
                    Content = ApplicationStatus.Connected,
                    Date = DateTimeOffset.Now,
                    Message = "Error on sync"
                };
            }
        }

        private static ApplicationWebCommandResponse<ApplicationStatus> StopConnection(string[] arg)
        {
            _service.GetInstance().Disconect();
            return new ApplicationWebCommandResponse<ApplicationStatus>
            {
                Content = ApplicationStatus.Connected,
                Date = DateTimeOffset.Now,
                Message = "Disconnecting"
            };
        }

        private static int ValidatePortConfig()
        {
            string settingPort;

            try
            {
                settingPort = ConfigurationManager.AppSettings["WebSocketPort"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            if (!int.TryParse(settingPort, out int value))
            {
                Console.WriteLine($"Invalid config port {settingPort}");
                throw new InvalidCastException($"Invalid config for WebSocketPort. Value :{settingPort}");
            }

            if (value == 0)
            {
                Console.WriteLine($"Invalid config port {settingPort}");
                throw new ArgumentException($"Invalid value for WebSocketPort. Value :{settingPort}");
            }

            return value;
        }

        private static void OnAccept(IAsyncResult result)
        {
            Console.WriteLine("Recieving new message");

            byte[] buffer = new byte[1024];
            try
            {
                Socket client = null;
                string headerResponse = "";
                if (_applicationSocket.ServerSocket != null && _applicationSocket.ServerSocket.IsBound)
                {
                    client = _applicationSocket.ServerSocket.EndAccept(result);
                    int bufferClientDataLength = client.Receive(buffer);
                    headerResponse = (System.Text.Encoding.UTF8.GetString(buffer)).Substring(0, bufferClientDataLength);
                }

                if (client != null)
                {
                    SendClientResponse(headerResponse, client);

                    ApplicationWebCommandResponse<ApplicationStatus> serviceResponse = PerformMessageAction(headerResponse);
                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(serviceResponse);

                    client.Send(UtilsString.GetFrameFromString(message));
                    System.Threading.Thread.Sleep(SleepIntervalOnMilliseconds);
                }
            }
            catch (SocketException exception)
            {
                Console.WriteLine($"{DateTimeOffset.Now.ToString()} : Error occurs when resetting service \n {exception.Message}");
            }
            finally
            {
                if (_applicationSocket.ServerSocket != null && _applicationSocket.ServerSocket.IsBound)
                {
                    _applicationSocket.ServerSocket.BeginAccept(null, 0, OnAccept, null);
                }
            }
        }

        private static ApplicationWebCommandResponse<ApplicationStatus> PerformMessageAction(string browserMessage)
        {
            ApplicationWebCommandResponse<ApplicationStatus> serviceResponse = new ApplicationWebCommandResponse<ApplicationStatus>();
            BasicOperationResponse<ApplicationWebCommand> incomingBasicOperationResponse = CastWebMessage(browserMessage);
            try
            {
                if (!incomingBasicOperationResponse.Success)
                {
                    serviceResponse =
                        new ApplicationWebCommandResponse<ApplicationStatus>()
                        {
                            Date = DateTimeOffset.Now,
                            Message = "CommandNoSupported",
                            Content = ApplicationStatus.StopDisconnected
                        };
                    Console.WriteLine($"Invalid Message for Application");
                }
                else
                {
                    ApplicationWebCommand incomingCommand = incomingBasicOperationResponse.OperationResult;
                    serviceResponse = _applicationCommands[incomingCommand.Command].Invoke(new[] { incomingCommand.Handler.ToString() });
                }
            }
            catch (Exception e)
            {
                serviceResponse.Content = ApplicationStatus.Disconnected;
                serviceResponse.Date = DateTimeOffset.Now;
                serviceResponse.Message = e.Message;
                Console.WriteLine(e.Message);
            }

            return serviceResponse;
        }

        private static void SendClientResponse(string headerResponse, Socket client)
        {
            const string newLine = "\r\n";
            string templateResponse = "HTTP/1.1 101 Switching Protocols" + newLine
                                                                 + "Upgrade: websocket" + newLine
                                                                 + "Connection: Upgrade" + newLine
                                                                 + "Sec-WebSocket-Accept: {replaceValue}" + newLine + newLine; ;

            client.Send(System.Text.Encoding.UTF8.GetBytes("Ok"));
        }

        private static BasicOperationResponse<ApplicationWebCommand> CastWebMessage(string browserMessage)
        {
            try
            {
                ApplicationWebCommand command = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationWebCommand>(browserMessage);
                return BasicOperationResponse<ApplicationWebCommand>.Ok(command);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return BasicOperationResponse<ApplicationWebCommand>.Fail("Invalid Command");
            }
        }
    }
}

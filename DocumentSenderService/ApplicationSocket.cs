using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSenderService
{
    internal class ApplicationSocket
    {
        public readonly Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        public readonly SHA1 Sha1 = SHA1CryptoServiceProvider.Create();
        public readonly string Guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public ApplicationSocket(int socketPort, AsyncCallback callbackFunction)
        {
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, socketPort));
            ServerSocket.Listen(1);
            ServerSocket.BeginAccept(null, 0, callbackFunction, null);
            Console.WriteLine($"Open WebSocket on port :{socketPort}");
        }
    }
}

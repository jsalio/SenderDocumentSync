using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DocumentSender;
using Optional.Unsafe;

namespace DocumentSenderService
{
    public partial class Service1 : ServiceBase
    {
        private OnBaseReleaser _onBaseReleaser;

        public Service1()
        {
            InitializeComponent();
            _onBaseReleaser = new OnBaseReleaser();
        }

        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
            _onBaseReleaser.Disconect();
        }

        internal void OnDebug()
        {
            
        }

        internal ApplicationWebCommandResponse<ApplicationStatus> SendDocument(long handler)
        {
            SendDocumentToOnBase _sender = new SendDocumentToOnBase();
            var newHandler = _sender.SaveDocument(handler, _onBaseReleaser);
            ValidateResult(newHandler);
            return new ApplicationWebCommandResponse<ApplicationStatus>
            {
                Content = ApplicationStatus.Connected,
                Date = DateTimeOffset.Now,
                Message = newHandler.ValueOrFailure().ToString()
            };
        }

        private void ValidateResult(Optional.Option<long, Exception> newHandler)
        {
            if (!newHandler.HasValue)
            {
                newHandler.MatchNone(none => throw new Exception($"Error on send document {none.Message}"));
            }
        }

        public OnBaseReleaser GetInstance() 
            => _onBaseReleaser;

    }
}

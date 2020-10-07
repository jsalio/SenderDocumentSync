using System;

namespace DocumentSenderService
{
    internal class ApplicationWebCommand
    {
        /// <summary>
        /// Date to send command
        /// </summary>
        public DateTimeOffset Date { get; set; }
        /// <summary>
        /// the command to execute
        /// </summary>
        public ApplicationCommand Command { get; set; }

        public long Handler { get; set; }
    }
}
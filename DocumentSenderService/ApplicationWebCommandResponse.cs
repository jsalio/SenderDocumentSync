using System;

namespace DocumentSenderService
{
    /// <summary>
    /// Response for command execute successfully
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationWebCommandResponse<T>
    {
        /// <summary>
        /// Date of execution
        /// </summary>
        public DateTimeOffset Date { get; set; }
        /// <summary>
        /// Response content
        /// </summary>
        public T Content { get; set; }
        /// <summary>
        /// Additional messages
        /// </summary>
        public string Message { get; set; } 
    }
}
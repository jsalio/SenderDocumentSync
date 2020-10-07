namespace DocumentSender.RestManagers
{
    public class CaptureClientRestResponse
    {
        /// <summary>
        /// Represents a HTTP Response status
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Content information about the requested data if the request successful.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Contains the information about the error if the request failed.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
namespace DocumentSender
{
    /// <summary>
    /// Represents a client for send request 
    /// </summary>
    public sealed class CaptureRestClient
    {
        /// <summary>
        /// Server URL. Example: <example>http://localhost:80</example>
        /// </summary>
        public string ClientServer { get; set; }
        
        /// <summary>
        /// Path  to  resource on server. Example: <example>api/v1/notification</example>  
        /// </summary>
        public string ClientResource { get; set; }

        /// <summary>
        /// Represents a type of HTTP verb 
        /// For more info See. "https://www.restapitutorial.com/lessons/httpmethods.html" 
        /// </summary>
        public MethodType MethodType { get; set; }

        /// <summary>
        /// Represents a time of wait to complete operation. Example: <example>30 seconds</example>
        /// </summary>
        public float Timeout { get; set; }
    }
}
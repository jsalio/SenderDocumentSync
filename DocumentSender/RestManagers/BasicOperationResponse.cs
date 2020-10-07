namespace DocumentSender
{
    /// <summary>
    /// Represents the response of an use case that works with <see cref="T"/> model.
    /// </summary>
    public sealed class BasicOperationResponse<T>
    {
        private BasicOperationResponse(string message, bool success, T operationResult)
        {
            Message = message;
            Success = success;
            OperationResult = operationResult;
        }

        /// <summary>
        /// Represents the operation message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Represents if the operation was successful
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Represents the operation result
        /// </summary>
        public T OperationResult { get; }

        /// <summary>
        /// Status code for response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResponse{T}"/> for success cases with a <see cref="T"/> default value.
        /// </summary>
        /// <returns>An instance of <see cref="BasicOperationResponse{T}"/> successfully</returns>
        public static BasicOperationResponse<T> Ok()
            => new BasicOperationResponse<T>(string.Empty, true, default(T));
        
        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResponse{T}"/> for success cases.
        /// </summary>
        /// <param name="operationResult">An <see cref="T"/></param>
        /// <returns>An instance of <see cref="BasicOperationResponse{T}"/> successfully</returns>
        public static BasicOperationResponse<T> Ok(T operationResult)
            => new BasicOperationResponse<T>(string.Empty, true, operationResult);

        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResponse{T}"/> for fail cases.
        /// </summary>
        /// <param name="message"> An operation's message</param>
        /// <returns>An instance of <see cref="BasicOperationResponse{T}"/> failed</returns>
        public static BasicOperationResponse<T> Fail(string message)
            => new BasicOperationResponse<T>(message, false, default(T));
    }
}
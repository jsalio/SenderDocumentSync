namespace DocumentSender
{
    /// <summary>
    /// Represents a Request
    /// </summary>
    /// <typeparam name="T">A type of data to send <see cref="CaptureRestClientRequest{T}"/></typeparam>
    public sealed class CaptureRestClientRequest<T>
    {
        /// <summary>
        /// A <see cref="CaptureRestClient"/>
        /// </summary>
        public CaptureRestClient CaptureClient { get; set; }

        /// <summary>
        /// Type of data to send. example: <example><see cref=" Core.Model.WebHooks.DocumentNotification"/></example>
        /// </summary>
        public T RequestBody { get; set; }
    }
}
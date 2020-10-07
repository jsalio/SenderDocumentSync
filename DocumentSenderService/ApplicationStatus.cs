namespace DocumentSenderService
{
    /// <summary>
    /// Application status for show on the Web UI
    /// </summary>
    internal enum ApplicationStatus
    {
        /// <summary>
        /// service is running an the application is connector for release target
        /// </summary>
        Connected = 0,
        /// <summary>
        /// service is running an the application is disconnected for release target
        /// </summary>
        Disconnected = 1,
        /// <summary>
        /// service received command for stop connection when the service process still running
        /// </summary>
        StopDisconnected = 2
    }
}
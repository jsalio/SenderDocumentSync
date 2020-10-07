namespace DocumentSenderService
{
    /// <summary>
    /// Available command for this service
    /// </summary>
    public enum ApplicationCommand
    {
        /// <summary>
        /// Retrieve service status
        /// </summary>
        Send = 0,
        /// <summary>
        /// reset service resources
        /// </summary>
        Reset = 1,
        /// <summary>
        /// Stop service resources
        /// </summary>
        Stop=2,
        /// <summary>
        /// command for initialize connection
        /// </summary>
        Start = 3
    }
}
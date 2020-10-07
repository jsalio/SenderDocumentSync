namespace DocumentSender.Model
{
    /// <summary>
    /// Model keyword for prodoctivity
    /// </summary>
    public partial class Keyword
    {
        /// <summary>
        /// handler
        /// </summary>
        public long Handle { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        public long Order { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
}
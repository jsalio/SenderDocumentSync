namespace DocumentSender.Model
{
    /// <summary>
    /// Model for Prodoctivity
    /// </summary>
    public partial class KeywordData
    {
        /// <summary>
        /// a <see cref="KeywordMap"/>
        /// </summary>
        public KeywordMap[] KeywordMap { get; set; }
        /// <summary>
        /// a List of <see cref="Keyword"/>
        /// </summary>
        public Keyword[] Keywords { get; set; }
        /// <summary>
        /// Model for Prodoctivity Record
        /// </summary>
        public object[] RecordMap { get; set; }
        /// <summary>
        /// Model for prodoctivity Records
        /// </summary>
        public object[] Records { get; set; }
    }
}
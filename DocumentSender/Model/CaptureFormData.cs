using System.Collections.Generic;

namespace DocumentSender
{
    /// <summary>
    /// Represents a Capture form Data
    /// </summary>
    public sealed class CaptureFormData
    {
        /// <summary>
        /// Creates an instance of Capture form data
        /// </summary>
        /// <param name="keywords">Document's keywords</param>
        /// <param name="records">Documents's keywords record</param>
        public CaptureFormData(IEnumerable<CaptureKeyword> keywords, IEnumerable<CaptureGroup> records)
        {
            Keywords = keywords;
            Records = records;
        }

        /// <summary>
        /// Keywords
        /// </summary>
        public IEnumerable<CaptureKeyword> Keywords { get; }

        /// <summary>
        /// Groups of keywords
        /// </summary>
        public IEnumerable<CaptureGroup> Records { get; }
    }
}
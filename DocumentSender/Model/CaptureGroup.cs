using System.Collections.Generic;

namespace DocumentSender
{
    /// <summary>
    /// Represents a capture Keyword Record
    /// </summary>
    public sealed class CaptureGroup
    {
        /// <summary>
        /// Creates an instance of Capture Record
        /// </summary>
        public CaptureGroup()
            => Keywords = new List<CaptureKeyword>();

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Keywords
        /// </summary>
        public ICollection<CaptureKeyword> Keywords { get; }

        /// <summary>
        /// Adds a keywords to the collection
        /// </summary>
        /// <param name="keyword">Keyword to be added</param>
        public void AddKeyword(CaptureKeyword keyword)
            => Keywords.Add(keyword);

    }
}
using System.Collections.Generic;
using UnityApiWrapper.Models;

namespace DocumentSender
{
    /// <summary>
    /// Represents a new document to send to OnBase
    /// </summary>
    public class OnBaseNewDocument
    {
        /// <summary>
        /// the <see cref="DocumentTypeModel"/>
        /// </summary>
        public DocumentTypeModel DocumentType { get; set; }
        /// <summary>
        /// the list of pages
        /// </summary>
        public List<string> Pages { get; set; }
        /// <summary>
        /// the <see cref="List{T}"/> of <see cref="KeywordModel"/>
        /// </summary>
        public List<KeywordModel> Keywords { get; set; }
    }
}
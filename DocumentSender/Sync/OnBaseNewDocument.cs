using System.Collections.Generic;
using UnityApiWrapper.Models;

namespace DocumentSender
{
    public class OnBaseNewDocument
    {
        public DocumentTypeModel DocumentType { get; set; }
        public List<string> Pages { get; set; }
        public List<KeywordModel> Keywords { get; set; }
    }
}
using System;

namespace DocumentSender.Model
{
    /// <summary>
    /// Model for create a ProDoctitivty document
    /// </summary>
    public partial class ProdoctivityDocument
    {
        /// <summary>
        /// base64 Document
        /// </summary>
        public string[] PagesBase64 { get; set; }
        /// <summary>
        /// request content type
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Document handler <code>default is 0</code>
        /// </summary>
        public long DocumentHandle { get; set; }
        /// <summary>
        /// document type handler
        /// </summary>
        public long DocumentTypeHandle { get; set; }
        /// <summary>
        /// Last Updated
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// Created By
        /// </summary>
        public long CreatedBy { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }
        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }
        /// <summary>
        /// Version Date
        /// </summary>
        public DateTimeOffset VersionDate { get; set; }
        /// <summary>
        /// Document Type
        /// </summary>
        public string DocumentType { get; set; }
        /// <summary>
        /// Reference
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// KeywordData
        /// </summary>
        public KeywordData KeywordData { get; set; }
        ///// <summary>
        ///// GenerationToken
        ///// </summary>
        //public Guid GenerationToken { get; set; }
        /// <summary>
        /// Accesses
        /// </summary>
        public object[] Accesses { get; set; }
        ///// <summary>
        ///// TemplateDocumentHandle
        ///// </summary>
        //public object TemplateDocumentHandle { get; set; }
    }
}
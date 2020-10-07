using DocumentSender.Model;
using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityApiWrapper.Models;
using UnityApiWrapper.Models.Enums;
using UnityApiWrapper.Models.Models.PrimitiveDomain;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace DocumentSender
{
    public class SendDocumentToOnBase
    {
        private readonly string _documentTypeName;
        private readonly long _documentTypeId;
        private readonly Dictionary<string, string> _keywords;


        public SendDocumentToOnBase()
        {
            _documentTypeName = ConfigurationManager.AppSettings["documentTypeName"];
            _documentTypeId = int.Parse(ConfigurationManager.AppSettings["documentTypeId"]);
            _keywords = CastSettingToKeywords();
        }

        public Option<long, Exception> SaveDocument(long handler, OnBaseReleaser releaser)
        {
            var prodoctivityDocument = GetDocumentFromProdoctivity(handler);
            var obDocument = ConvertOnbaseDocument(prodoctivityDocument);

            var newHandler = releaser.GetInstance().GuardarDocumentoConPaginas(obDocument.DocumentType, obDocument.Pages,
                obDocument.Keywords, FileFormatType.Pdf);

            if (long.TryParse(newHandler, out _))
            {
                return Option.Some<long, Exception>(long.Parse(newHandler));
            }

            return Option.None<long, Exception>(new ArgumentException(newHandler));
        }

        private ProdoctivityDocument GetDocumentFromProdoctivity(long handler)
        {
            FindDocumentOnProdoctivity find = new FindDocumentOnProdoctivity();
            return find.GetProdoctivityDocument(new DocumentId(handler));
        }

        private OnBaseNewDocument ConvertOnbaseDocument(ProdoctivityDocument document)
        {
            DocumentTypeModel documentType = new DocumentTypeModel() { Id = new DocumentTypeId(_documentTypeId), Name = _documentTypeName };
            CaptureFormData data = GetDocumentData(document);
            return new OnBaseNewDocument
            {
                DocumentType = documentType,
                Pages = new List<string> { document.PagesBase64[0] },
                Keywords = _keywords.Select(x =>
                {
                    var cValue = data.Keywords.FirstOrDefault(y => y.Name == x.Key);
                   
                    return new KeywordModel
                    {
                        KeywordId = 0,
                        KeywordTypeGroup = "",
                        TypeName = x.Value,
                        Value = cValue != null ? cValue.Value : "" 
                    };
                }).ToList()
            };
        }

        private CaptureFormData GetDocumentData(ProdoctivityDocument document)
        {
            IEnumerable<CaptureKeyword> keywords = document.KeywordData.KeywordMap.Join(document.KeywordData.Keywords,
                map => map.Handle, val => val.Handle, (map, val) => new CaptureKeyword()
                {
                    Name = map.Name,
                    Value = val.Value
                }).ToList();
            return new CaptureFormData(keywords, new List<CaptureGroup>());
        }

        public Dictionary<string, string> CastSettingToKeywords()
            => ConfigurationManager.AppSettings["keywords"].Split(';').Select(x =>
                                                                               {
                                                                                   string[] currentPair = x.Split(',');
                                                                                   return new KeyValuePair<string, string>(currentPair[0], currentPair[1]);
                                                                               }).ToDictionary(y => y.Key, z => z.Value);
    }
}

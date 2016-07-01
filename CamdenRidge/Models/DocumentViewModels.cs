using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class DocumentViewModel : Document
    {
        public byte [] FileContents { get; set; }
    }

    public class AllDocumentsViewModel
    {
        public bool AllowCreate { get; set; }

        public IEnumerable<Document> Documents { get; set; }
    }
}
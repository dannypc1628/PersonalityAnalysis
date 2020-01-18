using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Models.ViewModels
{
    public class ImportSortGroupViewModel
    {
        public ResponseImportanceSort[] Sort { get; set; }

        public Question[] Question { get; set; }
    }
}
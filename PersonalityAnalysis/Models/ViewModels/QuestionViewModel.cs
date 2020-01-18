using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Models.ViewModels
{
	public class QuestionViewModel
	{
        public int ID { get; set; }

        [Display(Name = "題目")]
        public string Name { get; set; }

        public string SecendName { get; set; }

        [Display(Name = "題號")]
        public int? Number { get; set; }

        [Display(Name = "選項")]
        public QuestionViewModel[] Items { get; set; }	
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ID { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Models.ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "帳號")]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "名稱")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "再輸入一次密碼")]
        [Compare("Password",ErrorMessage = "兩次密碼並不相同")]
        public string SecendPassword { get; set; }

   
    }
}
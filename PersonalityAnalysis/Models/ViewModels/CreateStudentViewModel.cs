using PersonalityAnalysis.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Models.ViewModels
{
    public class CreateStudentViewModel
    {
        public Guid Lecture_Key { get; set; }

        [Display(Name = "公司/社團/學校")]
        [StringLength(50)]
        [Required(ErrorMessage = "公司/社團/學校欄位不可以是空白")]
        public string Compeny { get; set; }

        [StringLength(50)]
        [Display(Name = "員工編號/學號")]
        [Required(ErrorMessage = "員工編號/學號欄位不可以是空白")]
        public string Employee_ID { get; set; }

        [Required(ErrorMessage ="姓名欄位不可以是空白")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "生日")]
        [UIHint("Date")]
        [DataType(DataType.Date, ErrorMessage = "請輸入西元年生日，例：1999-01-01")]
        [Required(ErrorMessage = "生日欄位不可以是空白")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "性別")]
        [Range(1, 2,ErrorMessage ="請選擇性別")]
        public GenderEnum Gender { get; set; }

        [StringLength(50)]
        [Display(Name = "手機")]
        [Phone]
        [Required(ErrorMessage = "手機欄位不可以是空白")]
        public string Phone_Number { get; set; }

        [StringLength(50)]
        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage ="這不是電子信箱格式")]
        [Required(ErrorMessage = "電子信箱欄位不可以是空白")]
        public string Email { get; set; }
                
    }
}
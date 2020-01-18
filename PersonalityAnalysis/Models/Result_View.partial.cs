namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [MetadataType(typeof(Result_View_MetaData))]
    public partial class Result_View
    {

    }

    public partial class Result_View_MetaData
    {
        
        [Display(Name ="姓名")]
        public   string Student_Name { get; set; }

        [Display(Name = "學號")]
        public string Employee_ID { get; set; }
        [StringLength(255)]
        [Display(Name = "圓夢新思維工作首要條件")]
        public string Goal_Name { get; set; }

        public int? D { get; set; }

        public int? I { get; set; }

        public int? S { get; set; }

        public int? C { get; set; }

        [Display(Name = "業務能力")]
        public int? Ability { get; set; }

        [Display(Name = "橫座標")]
        public int? Horizontal_Score { get; set; }

        [Display(Name = "縱座標")]
        public int? Vertical_Score { get; set; }


        [Display(Name = "最重要")]
        public string T1_Name { get; set; }

        [Display(Name = "次重要")]
        public string T2_Name { get; set; }

        [Display(Name = "重要")]
        public string T3_Name { get; set; }

       
        public int Student_ID { get; set; }

        public int? DISC_Number { get; set; }
    }
}

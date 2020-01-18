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
        
        [Display(Name ="�m�W")]
        public   string Student_Name { get; set; }

        [Display(Name = "�Ǹ�")]
        public string Employee_ID { get; set; }
        [StringLength(255)]
        [Display(Name = "��ڷs����u�@���n����")]
        public string Goal_Name { get; set; }

        public int? D { get; set; }

        public int? I { get; set; }

        public int? S { get; set; }

        public int? C { get; set; }

        [Display(Name = "�~�ȯ�O")]
        public int? Ability { get; set; }

        [Display(Name = "��y��")]
        public int? Horizontal_Score { get; set; }

        [Display(Name = "�a�y��")]
        public int? Vertical_Score { get; set; }


        [Display(Name = "�̭��n")]
        public string T1_Name { get; set; }

        [Display(Name = "�����n")]
        public string T2_Name { get; set; }

        [Display(Name = "���n")]
        public string T3_Name { get; set; }

       
        public int Student_ID { get; set; }

        public int? DISC_Number { get; set; }
    }
}

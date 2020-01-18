namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Result_View
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Student_Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Goal_Name { get; set; }

        public int? D { get; set; }

        public int? I { get; set; }

        public int? S { get; set; }

        public int? C { get; set; }

        public int? Ability { get; set; }

        public int? Horizontal_Score { get; set; }

        public int? Vertical_Score { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string T1_Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string T2_Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string T3_Name { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Student_ID { get; set; }

        public int? DISC_Number { get; set; }

        [StringLength(50)]
        public string Employee_ID { get; set; }
    }
}

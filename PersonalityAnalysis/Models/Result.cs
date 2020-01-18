namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Student_ID { get; set; }

        public int? Goal_ID { get; set; }

        public int? D { get; set; }

        public int? I { get; set; }

        public int? S { get; set; }

        public int? C { get; set; }

        public int? Ability { get; set; }

        public int? Top_1 { get; set; }

        public int? Top_2 { get; set; }

        public int? Top_3 { get; set; }

        public int? Horizontal_Score { get; set; }

        public int? Vertical_Score { get; set; }

        public int? DISC_Number { get; set; }
    }
}

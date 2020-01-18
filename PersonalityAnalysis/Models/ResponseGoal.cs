namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseGoal")]
    public partial class ResponseGoal
    {
        public int ID { get; set; }

        public int Student_ID { get; set; }

        public int? First_Goal_ID { get; set; }

        [StringLength(250)]
        public string Why { get; set; }

        public virtual Student Student { get; set; }
    }
}

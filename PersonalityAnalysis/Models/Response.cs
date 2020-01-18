namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Response")]
    public partial class Response
    {
        public int ID { get; set; }

        public int Question_ID { get; set; }

        public int Student_ID { get; set; }

        public int Response_ID { get; set; }

        public virtual Question Question { get; set; }

        public virtual Student Student { get; set; }
    }
}

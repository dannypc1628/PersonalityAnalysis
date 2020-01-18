namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseImportanceSort")]
    public partial class ResponseImportanceSort
    {
        public int Student_ID { get; set; }

        public int Response_ID { get; set; }

        public int Number { get; set; }

        public int ID { get; set; }
    }
}

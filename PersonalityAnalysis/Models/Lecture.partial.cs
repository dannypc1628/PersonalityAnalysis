namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [MetadataType(typeof(Lecture_MetaData))]
    public partial class Lecture
    {

    }

    public partial class Lecture_MetaData
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "�Z�ŦW��")]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "�إߤ��")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Create_Date { get; set; }

        public Guid Key { get; set; }
    }
}

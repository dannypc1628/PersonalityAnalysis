namespace PersonalityAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Response_View
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string 題目 { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool 反轉 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int 選項編號 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string 選項名稱 { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int 學生編號 { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int 題目編號 { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int 試卷編號 { get; set; }
    }
}

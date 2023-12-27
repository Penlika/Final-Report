namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOGOIMG")]
    public partial class LOGOIMG
    {
        [Key]
        [StringLength(40)]
        public string COMPANY { get; set; }

        public string LOGO { get; set; }
    }
}

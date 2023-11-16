namespace FundamentalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(20)]
        public string PASSWORD { get; set; }
    }
}

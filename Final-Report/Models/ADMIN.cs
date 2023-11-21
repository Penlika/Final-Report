namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        public string PICTURES { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string USERNAME { get; set; }

        public virtual USER USER { get; set; }
    }
}

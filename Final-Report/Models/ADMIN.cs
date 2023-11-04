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
        public int? ID { get; set; }

        [Key]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        public virtual USER USER { get; set; }
    }
}

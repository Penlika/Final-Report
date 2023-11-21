namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMER")]
    public partial class CUSTOMER
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string NAME { get; set; }

        public DateTime? DATEOFBIRTH { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string USERNAME { get; set; }

        public string PICTURES { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(10)]
        public string PHONENUMBER { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        public virtual USER USER { get; set; }
    }
}

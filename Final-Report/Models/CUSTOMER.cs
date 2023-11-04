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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PHONENUMBER { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string ADDRESS { get; set; }

        public int? BOOKINGLISTS { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual USER USER { get; set; }
    }
}

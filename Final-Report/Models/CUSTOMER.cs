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
        public int? ID { get; set; }

        [StringLength(40)]
        public string NAME { get; set; }

        public DateTime? DATEOFBIRTH { get; set; }

        [Key]
        [StringLength(20)]
        public string USERNAME { get; set; }

        public string PICTURES { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(10)]
        public string PHONENUMBER { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(50)]
        public string CARDNAME { get; set; }

        public int? CARDNUM { get; set; }

        public DateTime? EXPDATE { get; set; }

        public int? SECURNUM { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}

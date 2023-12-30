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

        [StringLength(40)]
        public string NAME { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATEOFBIRTH { get; set; }

        [StringLength(20)]
        public string USERNAME { get; set; }

        public string PICTURES { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(10)]
        public string PHONENUMBER { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(50)]
        public string CARDNAME { get; set; }

        [StringLength(16)]
        public string CARDNUM { get; set; }

        public DateTime? EXPDATE { get; set; }

        [StringLength(3)]
        public string SECURNUM { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}

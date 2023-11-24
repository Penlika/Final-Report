namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAYING")]
    public partial class PAYING
    {
        public int ID { get; set; }

        public int? IDCUSTOMER { get; set; }

        public int? IDHOTELBOOK { get; set; }

        public int? IDFLIGHTBOOK { get; set; }

        public int? IDPACKAGEBOOK { get; set; }

        [StringLength(50)]
        public string CARDNAME { get; set; }

        public int? CARDNUM { get; set; }

        public DateTime? EXPDATE { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual BOOKINGFLIGHT BOOKINGFLIGHT { get; set; }

        public virtual BOOKINGHOTEL BOOKINGHOTEL { get; set; }

        public virtual BOOKINGPACKAGE BOOKINGPACKAGE { get; set; }
    }
}

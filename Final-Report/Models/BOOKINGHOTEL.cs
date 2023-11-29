namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOKINGHOTEL")]
    public partial class BOOKINGHOTEL
    {
        [Key]
        public int IDRECEIPT { get; set; }

        public int? IDHOTEL { get; set; }

        public int? IDCUSTOMER { get; set; }

        public DateTime? CHECKINDATE { get; set; }

        public DateTime? CHECKOUTDATE { get; set; }

        public int NUMOFPERSON { get; set; }

        public int? ROOM { get; set; }

        public double TOTALPRICE { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

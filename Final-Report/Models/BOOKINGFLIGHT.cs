namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOKINGFLIGHT")]
    public partial class BOOKINGFLIGHT
    {
        [Key]
        public int IDRECEIPT { get; set; }

        public int? IDFLIGHT { get; set; }

        public int? IDCUSTOMER { get; set; }

        public string BOOKING_DETAIL { get; set; }

        public DateTime? BOOKINGDATE { get; set; }

        public double TOTALPRICE { get; set; }

        public int NUMOFPERSON { get; set; }

        [Required]
        [StringLength(20)]
        public string STATUS { get; set; }

        public virtual USER USER { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }
    }
}

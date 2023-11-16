namespace FundamentalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOKINGHOTEL")]
    public partial class BOOKINGHOTEL
    {
        public int? IDHOTEL { get; set; }

        public int? IDCUSTOMER { get; set; }

        public string BOOKING_DETAIL { get; set; }

        [Key]
        [Column(Order = 0)]
        public double TOTALPRICE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NUMOFPERSON { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string STATUS { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

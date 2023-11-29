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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKINGFLIGHT()
        {
            APPROVE = new HashSet<APPROVE>();
        }

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

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVE> APPROVE { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }
    }
}

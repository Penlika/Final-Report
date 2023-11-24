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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKINGHOTEL()
        {
            APPROVE = new HashSet<APPROVE>();
            PAYING = new HashSet<PAYING>();
        }

        [Key]
        public int IDRECEIPT { get; set; }

        public int? IDHOTEL { get; set; }

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

        public virtual HOTEL HOTEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYING> PAYING { get; set; }
    }
}

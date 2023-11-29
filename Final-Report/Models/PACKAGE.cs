namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PACKAGE")]
    public partial class PACKAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACKAGE()
        {
            BOOKINGPACKAGE = new HashSet<BOOKINGPACKAGE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DESTINATION { get; set; }

        public int? IDFLIGHT { get; set; }

        public int? IDHOTEL { get; set; }

        public string PICTURES { get; set; }

        [StringLength(250)]
        public string INFORMATION { get; set; }

        public int? PROLONG { get; set; }

        public double PRICE_PER_PERSON { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGPACKAGE> BOOKINGPACKAGE { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

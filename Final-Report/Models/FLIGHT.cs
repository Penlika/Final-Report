namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FLIGHT")]
    public partial class FLIGHT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FLIGHT()
        {
            BOOKINGFLIGHT = new HashSet<BOOKINGFLIGHT>();
            PACKAGE = new HashSet<PACKAGE>();
        }

        public int ID { get; set; }

        [StringLength(40)]
        public string COMPANY { get; set; }

        public string LOGO { get; set; }

        public DateTime DEPARTURE { get; set; }

        public DateTime ARRIVAL { get; set; }

        [Required]
        [StringLength(100)]
        public string FROM { get; set; }

        [Required]
        [StringLength(100)]
        public string TO { get; set; }

        public double PRICE_PER_PERSON { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGFLIGHT> BOOKINGFLIGHT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGE { get; set; }
    }
}

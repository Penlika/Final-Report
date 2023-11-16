namespace FundamentalProject.Models
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
            BOOKINGFLIGHTs = new HashSet<BOOKINGFLIGHT>();
            PACKAGEs = new HashSet<PACKAGE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string COMPANY { get; set; }

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
        public virtual ICollection<BOOKINGFLIGHT> BOOKINGFLIGHTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGEs { get; set; }
    }
}

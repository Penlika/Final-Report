namespace FundamentalProject.Models
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
            BOOKINGPACKAGEs = new HashSet<BOOKINGPACKAGE>();
            COMMENTANDRATINGs = new HashSet<COMMENTANDRATING>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string PACKAGENAME { get; set; }

        public string PICTURES { get; set; }

        public int? IDFLIGHT { get; set; }

        public int? IDHOTEL { get; set; }

        [Required]
        [StringLength(200)]
        public string DESTINATION { get; set; }

        [Required]
        [StringLength(250)]
        public string INFORMATION { get; set; }

        [Required]
        [StringLength(50)]
        public string PROLONG { get; set; }

        public double PACKAGEPRICE_PER_PERSON { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }

        public virtual HOTEL HOTEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGPACKAGE> BOOKINGPACKAGEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }
    }
}

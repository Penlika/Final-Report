namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PACKAGES")]
    public partial class PACKAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACKAGE()
        {
            COMMENTANDRATINGs = new HashSet<COMMENTANDRATING>();
            CUSTOMERs = new HashSet<CUSTOMER>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string PACKAGENAME { get; set; }

        public int? IDFLIGHT { get; set; }

        public int? IDHOTEL { get; set; }

        [Required]
        [StringLength(250)]
        public string INFORMATION { get; set; }

        public double PACKAGEPRICE_PER_PERSON { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }

        public virtual HOTEL HOTEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER> CUSTOMERs { get; set; }
    }
}

namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOTELS")]
    public partial class HOTEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOTEL()
        {
            PACKAGES = new HashSet<PACKAGE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string ADDRESS { get; set; }

        [Required]
        [StringLength(250)]
        public string INFORMATION { get; set; }

        public double PRICE_PER_PERSON { get; set; }

        public int? ROOM_AVAILABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGES { get; set; }
    }
}

namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOTEL")]
    public partial class HOTEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOTEL()
        {
            BOOKINGHOTEL = new HashSet<BOOKINGHOTEL>();
            COMMENTANDRATING = new HashSet<COMMENTANDRATING>();
            PACKAGE = new HashSet<PACKAGE>();
        }

        public int ID { get; set; }

        public string PICTURE1 { get; set; }

        public string PICTURE2 { get; set; }

        public string PICTURE3 { get; set; }

        public string PICTURE4 { get; set; }

        public string PICTURE5 { get; set; }

        public string PICTURE6 { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [Required]
        [StringLength(300)]
        public string ADDRESS { get; set; }

        public string INFORMATION { get; set; }

        public double PRICE_PER_PERSON { get; set; }

        public int ROOM_AVAILABLE { get; set; }

        public int? RATING { get; set; }

        public DateTime? MODIFIEDDAY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGHOTEL> BOOKINGHOTEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATING { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGE { get; set; }
    }
}

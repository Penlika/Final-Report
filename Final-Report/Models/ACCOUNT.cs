namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACCOUNT")]
    public partial class ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT()
        {
            ADMIN = new HashSet<ADMIN>();
            APPROVE = new HashSet<APPROVE>();
            BOOKINGFLIGHT = new HashSet<BOOKINGFLIGHT>();
            BOOKINGHOTEL = new HashSet<BOOKINGHOTEL>();
            BOOKINGPACKAGE = new HashSet<BOOKINGPACKAGE>();
            COMMENTANDRATING = new HashSet<COMMENTANDRATING>();
            CUSTOMER = new HashSet<CUSTOMER>();
            MANAGING = new HashSet<MANAGING>();
            PAYING = new HashSet<PAYING>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(30)]
        public string PASSWORD { get; set; }
        [NotMapped]
        [Compare("PASSWORD", ErrorMessage = "Confirm password must not be empty")]
        public string CONFIRMPASSWORD { get; set; }

        [Required]
        [StringLength(50)]
        public string ROLE { get; set; } = "customer";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMIN> ADMIN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVE> APPROVE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGFLIGHT> BOOKINGFLIGHT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGHOTEL> BOOKINGHOTEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGPACKAGE> BOOKINGPACKAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATING { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER> CUSTOMER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MANAGING> MANAGING { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYING> PAYING { get; set; }
    }
}
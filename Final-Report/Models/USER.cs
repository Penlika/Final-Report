namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            BOOKING_FORM = new HashSet<BOOKING_FORM>();
            ADMINs = new HashSet<ADMIN>();
            COMMENTANDRATINGs = new HashSet<COMMENTANDRATING>();
            CUSTOMERs = new HashSet<CUSTOMER>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Username must not be empty")]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "Password must not be empty")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
     ErrorMessage = "Password must have a capital letter, a number and a special character")]
        [StringLength(30, ErrorMessage = "Password must be from 8 to 30 characters", MinimumLength = 8)]
        public string PASSWORD { get; set; }

        [NotMapped]
        [Compare("PASSWORD", ErrorMessage = "Confirm password must not be empty")]
        public string CONFIRMPASSWORD { get; set; }

        [Required(ErrorMessage = "Email must not be empty")]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING_FORM> BOOKING_FORM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMIN> ADMINs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER> CUSTOMERs { get; set; }
    }
}

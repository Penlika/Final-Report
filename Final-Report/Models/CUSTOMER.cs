namespace FundamentalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMER")]
    public partial class CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            BOOKINGFLIGHTs = new HashSet<BOOKINGFLIGHT>();
            BOOKINGHOTELs = new HashSet<BOOKINGHOTEL>();
            BOOKINGPACKAGEs = new HashSet<BOOKINGPACKAGE>();
            COMMENTANDRATINGs = new HashSet<COMMENTANDRATING>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "The Username cannot be empty")]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "The Password cannot be empty")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be 8 to 20 characters long")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Password is not in the correc format")]
        public string PASSWORD { get; set; }

        [NotMapped]
        [Compare("PASSWORD", ErrorMessage = "The re-entered password does not match the password")]
        public string CONFIRMPASSWORD { get; set; }

        public string PICTURES { get; set; }

        [Required(ErrorMessage = "The Email cannot be empty")]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "The Phone number cannot be empty")]
        [StringLength(10)]
        public string PHONENUMBER { get; set; }

        [Required(ErrorMessage = "The Address cannot be empty")]
        [StringLength(100)]
        public string ADDRESS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGFLIGHT> BOOKINGFLIGHTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGHOTEL> BOOKINGHOTELs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKINGPACKAGE> BOOKINGPACKAGEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }
    }
}

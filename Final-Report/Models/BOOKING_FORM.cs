namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BOOKING_FORM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPACKAGE { get; set; }

        public int? IDCUSTOMER { get; set; }

        [Required]
        public string BOOKING_DETAIL { get; set; }

        public double TOTALPRICE { get; set; }

        public int NUMOFPERSON { get; set; }

        [Required]
        [StringLength(20)]
        public string STATUS { get; set; }

        public virtual USER USER { get; set; }
    }
}

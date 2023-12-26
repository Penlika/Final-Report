namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMENTANDRATING")]
    public partial class COMMENTANDRATING
    {
        public int ID { get; set; }

        public int? IDCUSTOMER { get; set; }

        public int? IDHOTEL { get; set; }

        [Required]
        [StringLength(400)]
        public string COMMENT { get; set; }

        public int STAR_RATING { get; set; }

        public DateTime? DATECREATE { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

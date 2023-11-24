namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("APPROVE")]
    public partial class APPROVE
    {
        public int ID { get; set; }

        public int? IDADMIN { get; set; }

        public int? IDHOTELBOOK { get; set; }

        public int? IDFLIGHTBOOK { get; set; }

        public int? IDPACKAGEBOOK { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual BOOKINGFLIGHT BOOKINGFLIGHT { get; set; }

        public virtual BOOKINGHOTEL BOOKINGHOTEL { get; set; }

        public virtual BOOKINGPACKAGE BOOKINGPACKAGE { get; set; }
    }
}

namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MANAGING")]
    public partial class MANAGING
    {
        public int ID { get; set; }

        public int? IDADMIN { get; set; }

        public int? IDHOTEL { get; set; }

        public int? IDFLIGHT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

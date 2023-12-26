namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FLIGHTIMG")]
    public partial class FLIGHTIMG
    {
        public int ID { get; set; }

        public int? IDFLIGHT { get; set; }

        public string IMAGEURL { get; set; }

        public virtual FLIGHT FLIGHT { get; set; }
    }
}

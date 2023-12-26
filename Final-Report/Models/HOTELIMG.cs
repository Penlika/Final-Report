namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOTELIMG")]
    public partial class HOTELIMG
    {
        public int ID { get; set; }

        public int? IDHOTEL { get; set; }

        public string IMAGEURL { get; set; }

        public virtual HOTEL HOTEL { get; set; }
    }
}

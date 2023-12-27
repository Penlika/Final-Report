namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string PICTURES { get; set; }

        [StringLength(20)]
        public string USERNAME { get; set; }

        [StringLength(40)]
        public string EMAIL { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}

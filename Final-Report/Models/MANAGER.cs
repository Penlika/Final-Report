namespace Final_Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MANAGER")]
    public partial class MANAGER
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string PICTURES { get; set; }

        [StringLength(20)]
        public string USERNAME { get; set; }

        [StringLength(30)]
        public string NAME { get; set; }

        [StringLength(40)]
        public string EMAIL { get; set; }

        [StringLength(10)]
        public string PHONENUMBER { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}

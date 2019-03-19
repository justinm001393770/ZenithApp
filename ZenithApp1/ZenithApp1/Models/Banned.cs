namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banned")]
    public partial class Banned
    {
        public long BannedID { get; set; }

        public long UserID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartBannedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndBannedDate { get; set; }

        public virtual User User { get; set; }
    }
}

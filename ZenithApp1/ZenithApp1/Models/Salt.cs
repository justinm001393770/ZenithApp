namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salt")]
    public partial class Salt
    {
        public long SaltID { get; set; }

        public long? UserID { get; set; }

        [StringLength(256)]
        public string SaltName { get; set; }

        public string SaltValue { get; set; }

        public virtual User User { get; set; }
    }
}

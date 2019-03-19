namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Verification")]
    public partial class Verification
    {
        public long VerificationID { get; set; }

        public long UserID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? VerificationCreated { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? VerifiedDate { get; set; }

        public bool IsVerified { get; set; }

        public virtual User User { get; set; }
    }
}

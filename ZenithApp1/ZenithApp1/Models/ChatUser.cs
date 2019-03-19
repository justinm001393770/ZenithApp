namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChatUser
    {
        public long ChatUserID { get; set; }

        public long ChatID { get; set; }

        public long UserID { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual Chat Chat { get; set; }

        public virtual User User { get; set; }
    }
}

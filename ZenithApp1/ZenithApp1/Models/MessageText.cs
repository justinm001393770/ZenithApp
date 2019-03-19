namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageText")]
    public partial class MessageText
    {
        public long MessageTextID { get; set; }

        public long MessageID { get; set; }

        public long PosterID { get; set; }

        [Required]
        [StringLength(4000)]
        public string ChatText { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual Message Message { get; set; }

        public virtual User User { get; set; }
    }
}

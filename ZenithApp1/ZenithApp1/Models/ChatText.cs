namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatText")]
    public partial class ChatText
    {
        public long ChatTextID { get; set; }

        public long ChatID { get; set; }

        public long PosterID { get; set; }

        [Column("ChatText")]
        [Required]
        [StringLength(4000)]
        public string ChatText1 { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual Chat Chat { get; set; }

        public virtual User User { get; set; }
    }
}

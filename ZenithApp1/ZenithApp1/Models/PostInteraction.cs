namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostInteraction")]
    public partial class PostInteraction
    {
        [Key]
        public long PostCommentID { get; set; }

        public long PostID { get; set; }

        public long InteractionID { get; set; }

        public bool IsActive { get; set; }

        public bool IsReported { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual Interaction Interaction { get; set; }

        public virtual Post Post { get; set; }
    }
}

namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameLibrary")]
    public partial class GameLibrary
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameLibraryID { get; set; }

        public long GameID { get; set; }

        public long UserID { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }
    }
}

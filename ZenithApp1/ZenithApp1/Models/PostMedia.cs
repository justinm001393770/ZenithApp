namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using ViewModels;

    [Table("PostMedia")]
    public partial class PostMedia
    {
        public long PostMediaID { get; set; }

        public long PostID { get; set; }

        public long MediaID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Medium Medium { get; set; }

        public virtual Post Post { get; set; }

    }

    
}

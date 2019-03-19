namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Interaction")]
    public partial class Interaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Interaction()
        {
            PostInteractions = new HashSet<PostInteraction>();
        }

        public long InteractionID { get; set; }

        [Column("Interaction")]
        [Required]
        [StringLength(256)]
        public string Interaction1 { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostInteraction> PostInteractions { get; set; }
    }
}

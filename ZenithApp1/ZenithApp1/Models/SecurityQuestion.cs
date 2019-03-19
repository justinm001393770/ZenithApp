namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityQuestion")]
    public partial class SecurityQuestion
    {
        [Key]
        public long SecurityQuestionsID { get; set; }

        public long UserID { get; set; }

        public string Question1 { get; set; }

        public string Question2 { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public virtual User User { get; set; }
    }
}

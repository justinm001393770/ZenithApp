using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZenithApp1.Models
{
    [Table("Game")]
    public partial class DBGame
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 GameID { get; set; }
        public string GameName { get; set; }
        public string GameDesc { get; set; }
        public string GameImgURL { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime GameReleaseDate { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }
        

        public string getImagePath()
        {
            return "//images.igdb.com/igdb/image/upload/t_cover_big/" + GameImgURL + ".jpg";
        }

        public static DBGame getDBGameById(long id)
        {
            ZenithContext db = new ZenithContext();
            return db.Game.Find(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("resto_menus")]
    public class MenuPhotos
    {
        [Key]
        public int RempId  { get; set; } 
        public string RempThumbnailFilename { get; set; }
        public string RempPhotoFilename { get; set; }
        public bool RempPrimary { get; set; }
        public String RempUrl { get; set; }
        public int RempRemeId { get; set; }
    }
}

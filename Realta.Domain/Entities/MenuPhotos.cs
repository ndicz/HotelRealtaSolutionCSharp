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
        public int remp_id { get; set; } 
        public string remp_thumbnail_filename { get; set; }
        public string remp_photo_filename { get; set; }
        public bool remp_primary { get; set; }
        public String remp_url { get; set; }
        public int remp_reme_id { get; set; }
    }
}

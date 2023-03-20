using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("resto_menus")]
    public class RestoMenus
    {
        [Key]
        public int RemeFaciId { get; set; } = 2;
        public int RemeId { get; set; }
        public string RemeName { get; set; }
        public string RemeDescription { get; set; }  
        public decimal RemePrice { get; set; }
        public string RemeStatus { get; set; }
        public DateTime RemeModifiedDate { get; set; }
        public string? RemeType { get; set; }

        public string? RempPhotoFilename { get; set; }
        public int OrmeId { get; set; }

    }
}

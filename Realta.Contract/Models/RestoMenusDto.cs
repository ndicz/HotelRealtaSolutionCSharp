using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Realta.Contract.Models
{
    public class RestoMenusDto
    {
        public int RemeFaciId { get; set; } = 2;
        public int RemeId { get; set; }
        public string RemeName { get; set; }
        public string RemeDescription { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal RemePrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]

        public string RemeStatus { get; set; }
        public DateTime RemeModifiedDate { get; set; } = DateTime.Now;

        public string RemeType { get; set; }

        public string RempUrl { get; set; }
        public string RempPhotoFilename { get; set; }
    }
}

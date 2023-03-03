using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class RestoMenusDto
    {
        public int RemeFaciId { get; set; } = 2;
        public int RemeId { get; set; }
        public string RemeName { get; set; }
        public string RemeDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal RemePrice { get; set; }
        public string RemeStatus { get; set; }
        public DateTime RemeModifiedDate { get; set; } = DateTime.Now;

        public string RemeType { get; set; }
    }
}

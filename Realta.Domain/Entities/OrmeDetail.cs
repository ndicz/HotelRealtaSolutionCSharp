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
    public class OrmeDetail
    {
        [Key]
           public int OmdeId { get; set; }
           public decimal OrmePrice { get; set; }
           public Int16 OrmeQty { get; set; }
        public decimal OrmeSubtotal { get; set; }
           public decimal OrmeDiscount { get; set; }
           public int OmdeOrmeId { get; set; }
           public int OmdeRemeId { get; set; }
            
        public string? RemeName { get; set; }

    }
}

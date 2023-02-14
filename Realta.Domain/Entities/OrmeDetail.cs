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
           public int omde_id { get; set; }
           public decimal orme_price { get; set; }
           public Int16 orme_qty { get; set; }
           public decimal orme_subtotal { get; set; }
           public decimal orme_discount { get; set; }
           public int omde_orme_id { get; set; }
           public int omde_reme_id { get; set; }
    }
}

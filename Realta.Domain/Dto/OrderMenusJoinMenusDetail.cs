using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class OrderMenusJoinMenusDetail
    {
        public int orme_id { get; set; }

        public string? orme_order_number { get; set; }

        public decimal orme_total_amount { get; set; }
        public decimal orme_total_discount { get; set; }


        public string reme_name { get; set; }

        public int omde_id { get; set; }
        public decimal orme_price { get; set; }
        public short orme_qty { get; set; }
        public decimal orme_subtotal { get; set; }
        public int omde_orme_id { get; set; }
        public int omde_reme_id { get; set; }
    }
}

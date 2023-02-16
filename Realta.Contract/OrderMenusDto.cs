using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract
{
    public class OrderMenusDto
    {
        public int orme_id { get; set; }

        public string? orme_order_number { get; set; }
        public DateTime orme_order_date { get; set; }
        public short orme_total_item { get; set; }
        public decimal orme_total_discount { get; set; }
        public decimal orme_total_amount { get; set; }
        public string orme_pay_type { get; set; }
        public string? orme_cardnumber { get; set; }
        public string orme_is_paid { get; set; }
        public DateTime orme_modified_date { get; set; }
        public int orme_user_id { get; set; }
    }
}

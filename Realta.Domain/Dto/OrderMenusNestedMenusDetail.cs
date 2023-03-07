using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class OrderMenusNestedMenusDetail
    {
        public int orme_id { get; set; }

        public string? orme_order_number { get; set; }

        public decimal orme_total_amount { get; set; }
        public decimal orme_total_discount { get; set; }

        public virtual ICollection<OrmeDetail>? MenuDetail { get; set; }
    }
}

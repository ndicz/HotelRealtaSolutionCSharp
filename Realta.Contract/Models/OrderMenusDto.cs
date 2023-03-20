using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class OrderMenusDto
    {
        public int OrmeId { get; set; }

        public string? OrmeOrderNumber { get; set; }
        public DateTime OrmeOrderDate { get; set; }
        public short OrmeTotalItem { get; set; }
        public decimal OrmeTotalDiscount { get; set; }
        public decimal OrmeTotalAmount { get; set; }
        public string OrmePayType { get; set; }
        public string? OrmeCardnumber { get; set; }
        public string OrmeIsPaid { get; set; }
        public DateTime OrmeModifiedDate { get; set; }
        public int OrmeUserId { get; set; }
        public String OrmeStatus { get; set; }
        public String OrmeInvoice { get; set; }

    }
}

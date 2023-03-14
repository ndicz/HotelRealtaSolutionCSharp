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
    public class OrderMenus
    {
        

        [Key]
       
        public int OrmeId { get; set; }

        public string? OrmeOrderNumber { get; set; }
        public DateTime OrmeOrderDate { get; set; } //dia aliaskan dengan as
        public short OrmeTotalItem { get; set; }
        public decimal OrmeTotalDiscount { get; set; }
        public decimal OrmeTotalAmount { get; set; }

        public string OrmePayType { get; set; }
        public string? ormeCardnumber { get; set; }
        public string OrmeIsPaid { get; set; }
        public DateTime OrmeModifiedDate { get; set; }
        public int OrmeUserId { get; set; }
    }
}

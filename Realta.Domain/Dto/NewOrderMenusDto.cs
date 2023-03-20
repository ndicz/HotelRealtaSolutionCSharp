using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class NewOrderMenusDto
    {
        public Int32 OmdeRemeId { get; set; }
        public decimal OrmePrice { get; set; } 
        public short OrmeQty { get; set; }
        public decimal OrmeDiscount { get; set; }
        public string OrmePayType { get; set; }
        public string? OrmeCardnumber { get; set; }
        public string OrmeIsPaid { get; set; }
        public Int32 OrmeUserId { get; set; }
        public String OrmeStatus { get; set; }
        public int OrmeId { get; set; }



        //IEnumerator<int> o = null;
        // @omde_reme_id = 58
        //,@orme_price = 20000
        //,@orme_qty = 3
        //,@orme_discount = 5000
        //,@orme_pay_type = 'CA'
        //,@orme_cardnumber = 'kjk'
        //,@orme_is_paid = 'P'
        //,@orme_user_id = 1
        //,@orme_status = 'Ordered'
    }
}

    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract
{
    public class RestoMenusDto
    {
        public int reme_faci_id { get; set; } = 2;
        public int reme_id { get; set; }
        public string reme_name { get; set; }
        public string reme_description { get; set; }
        public decimal reme_price { get; set; }
        public string reme_status { get; set; }
        public DateTime reme_modified_date { get; set; }
    }
}

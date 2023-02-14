using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("resto_menus")]
    public class RestoMenus
    {
        [Key]
        public int reme_faci_id { get; set; }
        public int reme_id { get; set; }
        public string reme_name { get; set; }
        public string reme_description { get; set; }  
        public decimal reme_price { get; set; }
        public string reme_status { get; set; }
        public DateTime reme_modified_date { get; set; }
    }
}

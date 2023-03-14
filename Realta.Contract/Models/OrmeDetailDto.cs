using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class OrmeDetailDto
    {
        public int OmdeId { get; set; }
        public decimal OrmePrice { get; set; }
        public short OrmeQty { get; set; }
        public decimal OrmeSubtotal { get; set; }
        public decimal OrmeDiscount { get; set; }
        public int OmdeOrmeId { get; set; }
        public int OmdeRemeId { get; set; }
    }
}

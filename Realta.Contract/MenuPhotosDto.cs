using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract
{
    public class MenuPhotosDto
    {

        public int remp_id { get; set; }
        public string remp_thumbnail_filename { get; set; }
        public string remp_photo_filename { get; set; }
        public bool remp_primary { get; set; }
        public string remp_url { get; set; }
        public int remp_reme_id { get; set; }
    }
}


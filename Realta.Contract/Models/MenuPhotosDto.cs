using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class MenuPhotosDto
    {

        public int RempId { get; set; }

        public string RempThumbnailFilename { get; set; }
        public string RempPhotoFilename { get; set; }
        public bool RempPrimary { get; set; }
        public string remp_url { get; set; }
        public int remp_reme_id { get; set; }
    }
}


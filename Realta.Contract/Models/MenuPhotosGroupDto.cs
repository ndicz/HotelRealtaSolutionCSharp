using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class MenuPhotosGroupDto
    {
        [Required]
        public RestoMenusDto? MenuPhotosCreateDto { get; set; }


        public List<IFormFile>? AllPhotos { get; set; }
    }
}

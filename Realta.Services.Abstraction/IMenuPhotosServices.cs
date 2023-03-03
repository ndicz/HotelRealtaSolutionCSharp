 using Realta.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services.Abstraction
{
    public interface IMenuPhotosServices
    {
        public void InsertPhotoAndMenu(MenuPhotosGroupDto menuPhotosGroupDto, out int RemeId);
    }
}

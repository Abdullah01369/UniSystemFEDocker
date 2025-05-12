using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IAnnouncementService
    {

       // public Task<ICollection<AnnouncementModelDto>> GetAll();
        public Task<CustomResponseDto<List<AnnouncementModelDto>>> GetAll(string mail);
        public Task<bool> Save(AnnouncementModelDto modelDto);
        public Task<bool> Update(int id);
        public Task<bool> Delete(int id);
        
    }
}

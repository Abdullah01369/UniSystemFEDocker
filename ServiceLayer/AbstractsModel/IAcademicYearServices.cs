using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IAcademicYearServices
    {
        public Task<ICollection<AcademicYearListViewModel>> AcademicYearList(string email);
    }
}

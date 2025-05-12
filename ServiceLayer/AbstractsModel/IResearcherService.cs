using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IResearcherService
    {
        Task<CustomResponseDto<ResearcherInfoDto>> ResearcherInfo(string email);
        Task<CustomResponseDto<ResearchMetricDto>> GetMetrics(string email);
        Task<CustomResponseDto<EducationInfoDto>> GetEducInfo(string email);
        Task<CustomResponseDto<List<ResearchAreaDto>>> GetResearcherAreaInfo(string email);
        Task<CustomResponseDto<List<ResearcherExp>>> GetResearcherExp(string email);
        Task<CustomResponseDto<List<ResearcherPublication>>> GetPublication(string email);
        Task<CustomResponseDto<ContactResearcherDto>> GetContact(string email);




    }
}

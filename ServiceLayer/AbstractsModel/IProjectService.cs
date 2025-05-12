using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IProjectService
    {
        public Task<CustomResponseDto<ProjectModel>> GetProjectInfoById(int Id, string email);
        public Task<CustomResponseDto<List<ProjectModel>>> GetProjectList(string email);
        public Task<CustomResponseDto<List<StudentListForProjectDetail>>> GetStudentList(int Id, string email);
        public Task<string> DocumentCount(int Id, string email);
        Task<CustomResponseDto<StudentsInfoForAcademicianDto>> GetStudentByStudentNo(string No,string email);
        Task<CustomResponseDto<StudentRequest>> AddStudentToProject(StudentSavingProjectViewModel request);
    }
}

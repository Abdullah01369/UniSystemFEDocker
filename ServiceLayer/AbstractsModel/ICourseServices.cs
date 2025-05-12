using ServiceLayer.Models;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface ICourseServices
    {
        Task<CustomResponseDto<PaggingModel>> GetCourseList(string mail, string? page, string? pagesize, string? search);
        Task<CustomResponseDto<PaggingModel>> GetCourseList(string mail);
        Task<CustomResponseDto<PaggingModel>> SearchCourse(string mail, string input);
        Task<CustomResponseDto<CourseDto>> AddCourse(string mail, AddCourseDto courseDto);
        Task<CustomResponseDto<List<DepartmentListDto>>> GetAllDepartment(string email);
        Task<CustomResponseDto<EditCourseDto>> EditCourse(string mail, EditCourseDto courseDto);
        Task<CustomResponseDto<CourseDto>> GetCourse(string mail, int Id);
        Task<CustomResponseDto<CourseStatisticModel>> GetStatistic(string mail);






    }
}

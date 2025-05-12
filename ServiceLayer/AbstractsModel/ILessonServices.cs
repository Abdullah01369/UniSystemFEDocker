using ServiceLayer.Requests;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface ILessonServices
    {

        public Task<ICollection<ExamModelView>> ExamLessonListByStudent(LessonListForStudentByDateRequestModel model);
        Task<byte[]> DownloadLessonProgramAsync(string code);
        Task<string> TakeDepartmentCode(string username, string email);
        Task<List<DocumentListDto>> TakeStudentDoc(string email);
        Task<CustomResponseDto<NoData>> TakeNewRequestStudentDoc(TakeStudentDocModel model);
        //Task DownloadFile(int Id, string Mail);
        Task<Stream> DownloadFile(int Id, string Mail);
        Task<CustomResponseDto<GradutedInfoDto>> GetGradInfo(string email );







    }

}

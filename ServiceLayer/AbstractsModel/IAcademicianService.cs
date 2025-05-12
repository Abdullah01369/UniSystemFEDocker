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
    public interface IAcademicianService
    {
        public Task<ICollection<AcademicianLessonsDto>> AcademicianLessonList(string UserName, string Mail);
        public Task<IEnumerable<StudentsInfoForAcademicianDto>> StudentInfoByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request);
        public Task<IEnumerable<StudentExamListResponseModel>> StudentInfoExamsByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request);
        public Task<bool> SaveMidtermGrade(SavingMidtermGradeModel request);
        public Task<bool> SaveFinal(SaveFinalExamModel request);
        public Task<bool> SaveBut(SaveButModel request);
        Task<string> Ask(string prompt, string Mail);

    }
}

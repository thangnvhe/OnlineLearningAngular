using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IStudentExamRepository : IRepository<StudentExam, int>
    {
        Task<PagedResult<StudentExam>> GetPagedStudentExamByExamIdAsync(int examId, PagingFilterBase filters);
    }
}

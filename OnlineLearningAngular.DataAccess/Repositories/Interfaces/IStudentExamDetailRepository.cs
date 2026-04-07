using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IStudentExamDetailRepository : IRepository<StudentExamDetail, int>
    {
        Task<IEnumerable<StudentExamDetail>> GetStudentExamDetailsByStudentExamIdAsync(int studentExamId);
    }
}

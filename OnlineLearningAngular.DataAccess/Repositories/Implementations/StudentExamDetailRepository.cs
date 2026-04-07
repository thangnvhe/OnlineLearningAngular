using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class StudentExamDetailRepository : EfRepository<StudentExamDetail, int>, IStudentExamDetailRepository
    {
        public StudentExamDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StudentExamDetail>> GetStudentExamDetailsByStudentExamIdAsync(int studentExamId)
        {
            return await FindAll().Where(sed => sed.StudentExamId == studentExamId).ToListAsync();
        }
    }
}

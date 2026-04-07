using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class StudentExamRepository : EfRepository<StudentExam, int>, IStudentExamRepository
    {
        public StudentExamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<StudentExam>> GetPagedStudentExamByExamIdAsync(int examId, PagingFilterBase filters)
        {
            var query = FindAll().AsNoTracking().Where(se => se.ExamId == examId);

            if (!string.IsNullOrWhiteSpace(filters.SortBy))
            {
                query = filters.SortBy.ToLower() switch
                {
                    "score" => filters.IsDescending
                        ? query.OrderByDescending(se => se.Score)
                        : query.OrderBy(se => se.Score),
                        
                    _ => filters.IsDescending
                        ? query.OrderByDescending(se => se.Id)
                        : query.OrderBy(se => se.Id)
                };
            }
            else
            {
                query = filters.IsDescending
                        ? query.OrderByDescending(se => se.Id)
                        : query.OrderBy(se => se.Id);
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((filters.Page - 1) * filters.Size)
                .Take(filters.Size)
                .ToListAsync();

            var pagedResult = new PagedResult<StudentExam>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = filters.Page,
                PageSize = filters.Size,
            };

            return pagedResult;
        }
    }
}

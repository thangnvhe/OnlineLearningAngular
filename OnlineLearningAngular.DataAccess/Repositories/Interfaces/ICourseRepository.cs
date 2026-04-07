using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<PagedResult<Course>> GetPagedAsync(PagingFilterBase filters);
    }
}

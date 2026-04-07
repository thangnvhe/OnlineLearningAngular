using OnlineLearningAngular.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task SaveAsync(AppLog log);

        Task<(IEnumerable<AppLog> items, int totalCount)> GetPagedAsync(Expression<Func<AppLog, bool>>? filter = null, string? includeProperties = null, Func<IQueryable<AppLog>, IOrderedQueryable<AppLog>>? orderBy = null, int pageNumber = 1, int pageSize = 10);
    }
}

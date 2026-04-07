using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser, int>
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<PagedResult<ApplicationUser>> GetPagedAsync(PagingFilterBase filters);
    }
}

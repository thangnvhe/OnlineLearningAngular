using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface ILogService
    {
        Task LogInfo(string source, string action, string message, int? userId = null, string? ip = null);
        Task LogError(string source, string action, Exception ex, int? userId = null, string? ip = null);
        Task<ServiceResult<PagedResult<AppLog>>> GetPagedLogsAsync(
        Expression<Func<AppLog, bool>>? filter = null,
        int pageNumber = 1,
        int pageSize = 10);
    }
}

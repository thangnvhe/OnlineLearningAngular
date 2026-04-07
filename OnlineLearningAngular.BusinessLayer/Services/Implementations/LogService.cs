using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Enums;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepo;
        public LogService(ILogRepository logRepo) => _logRepo = logRepo;

        public async Task LogInfo(string source, string action, string message, int? userId = null,string? ip = null)
        {
            var log = new AppLog
            {
                Level = LogType.Infomation,
                Source = source,
                Action = action,
                Message = message,
                UserId = userId,
                IPAddress = ip
            };
            await _logRepo.SaveAsync(log);
        }

        public async Task LogError(string source, string action, Exception ex, int? userId = null, string? ip = null)
        {
            var log = new AppLog
            {
                Level = LogType.Error,
                Source = source,
                Action = action,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                UserId = userId,
                IPAddress = ip
            };
            await _logRepo.SaveAsync(log);
        }
        public async Task<ServiceResult<PagedResult<AppLog>>> GetPagedLogsAsync(
                    Expression<Func<AppLog, bool>>? filter = null,
                    int pageNumber = 1,
                    int pageSize = 10)
        {
            // Implementation: Validate input parameters
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100; 

            // Call Repository with paging parameters
            var (items, totalCount) = await _logRepo.GetPagedAsync(filter, null, null, pageNumber, pageSize);

            // Wrap in PagedResult DTO
            var result = new PagedResult<AppLog>
            {
                Items = items,
                TotalItems = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return ServiceResult<PagedResult<AppLog>>.Success(result);
        }
    }
}

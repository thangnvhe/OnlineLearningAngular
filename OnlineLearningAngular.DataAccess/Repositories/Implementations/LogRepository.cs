using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext _context; 
        public LogRepository(AppDbContext context) => _context = context;

        public async Task<(IEnumerable<AppLog> items, int totalCount)> GetPagedAsync(Expression<Func<AppLog, bool>>? filter = null, string? includeProperties = null, Func<IQueryable<AppLog>, IOrderedQueryable<AppLog>>? orderBy = null, int pageNumber = 1, int pageSize = 10)
        {
            // 1. Tính toán số dòng cần bỏ qua
            int skip = (pageNumber - 1) * pageSize;

            // 2. Lấy tổng số dòng (Cần thiết cho phân trang ở Front-end)
            // Lưu ý: Vì filter là Expression, đoạn này vẫn dùng LINQ để Count cho nhẹ. 
            // Nếu muốn SQL thuần 100%, bạn phải viết chuỗi SQL đếm riêng.
            IQueryable<AppLog> countQuery = _context.AppLogs;
            if (filter != null) countQuery = countQuery.Where(filter);
            int totalCount = await countQuery.CountAsync();

            // 3. Viết SQL thuần để lấy dữ liệu phân trang
            // Sử dụng OFFSET...FETCH NEXT là cách tối ưu nhất trong SQL Server
            string sql = @"SELECT * FROM AppLogs 
                   ORDER BY CreatedAt DESC 
                   OFFSET {0} ROWS 
                   FETCH NEXT {1} ROWS ONLY";

            // Thực thi SQL thuần và map vào Entity AppLog
            var items = await _context.AppLogs
                .FromSqlRaw(sql, skip, pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task SaveAsync(AppLog log)
        {
            _context.AppLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}

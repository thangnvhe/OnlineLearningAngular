using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class CourseRepository : EfRepository<Course, int>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<PagedResult<Course>> GetPagedAsync(PagingFilterBase filters)
        {
            var query = FindAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                var searchTerm = filters.Search.Trim().ToLower();
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(filters.SortBy))
            {
                query = filters.SortBy.ToLower() switch
                {
                    "name" => filters.IsDescending
                        ? query.OrderByDescending(c => c.Name)
                        : query.OrderBy(c => c.Name),
                        
                    "price" => filters.IsDescending
                        ? query.OrderByDescending(c => c.Price)
                        : query.OrderBy(c => c.Price),

                    "createdat" => filters.IsDescending
                        ? query.OrderByDescending(c => c.CreateAt)
                        : query.OrderBy(c => c.CreateAt),

                    _ => filters.IsDescending
                        ? query.OrderByDescending(c => c.Id)
                        : query.OrderBy(c => c.Id)
                };
            }
            else
            {
                // Mặc định sắp xếp theo Id nếu không truyền SortBy
                query = filters.IsDescending
                        ? query.OrderByDescending(c => c.Id)
                        : query.OrderBy(c => c.Id);
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((filters.Page - 1) * filters.Size)
                .Take(filters.Size)
                .ToListAsync();

            var pagedResult = new PagedResult<Course>
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

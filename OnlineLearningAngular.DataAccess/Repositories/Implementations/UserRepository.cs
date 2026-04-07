using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class UserRepository : EfRepository<ApplicationUser, int>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<PagedResult<ApplicationUser>> GetPagedAsync(PagingFilterBase filters)
        {
            var query = FindAll().AsNoTracking();

            if (filters.IsActive != null)
            {
                query = query.Where(u => u.IsActive == filters.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                var searchTerm = filters.Search.Trim().ToLower();
                query = query.Where(u =>
                      u.FullName.Contains(searchTerm) ||
                      u.Email.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(filters.SortBy))
            {
                query = filters.SortBy.ToLower() switch
                {
                    "name" => filters.IsDescending
                        ? query.OrderByDescending(u => u.FullName)
                        : query.OrderBy(u => u.FullName),

                    "email" => filters.IsDescending
                        ? query.OrderByDescending(u => u.Email)
                        : query.OrderBy(u => u.Email),

                    "dob" => filters.IsDescending
                        ? query.OrderByDescending(u => u.Dob)
                        : query.OrderBy(u => u.Dob),

                    _ => filters.IsDescending
                        ? query.OrderByDescending(u => u.Id)
                        : query.OrderBy(u => u.Id)
                };
            }
            else
            {
                // Sắp xếp mặc định khi người dùng không truyền tham số SortBy
                query = filters.IsDescending
                        ? query.OrderByDescending(u => u.Id)
                        : query.OrderBy(u => u.Id);
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((filters.Page - 1) * filters.Size)
                .Take(filters.Size)
                .ToListAsync();

            var pagedResult = new PagedResult<ApplicationUser>
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

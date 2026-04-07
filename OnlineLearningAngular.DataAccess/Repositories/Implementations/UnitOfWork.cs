using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
        }

        

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);    
        }
    }
}

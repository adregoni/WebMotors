using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMotors.Domain.Contracts.UnityOfWork;

namespace WebMotors.Data.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context) => _context = context;

        public async Task<int> CommitAsync() => await _context?.SaveChangesAsync();

        public void Rollback() => _context?.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
    }
}

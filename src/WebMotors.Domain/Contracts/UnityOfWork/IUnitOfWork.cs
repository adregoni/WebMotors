using System.Threading.Tasks;

namespace WebMotors.Domain.Contracts.UnityOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

        void Rollback();
    }
}

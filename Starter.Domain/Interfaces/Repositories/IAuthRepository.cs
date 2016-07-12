using Starter.Domain.Entities;

namespace Starter.Domain.Interfaces.Repositories
{
    public interface IAuthRepository:IRepositoryBase<User>
    {
        void Register(User model);
        User Login(string username, string password);
    }
}

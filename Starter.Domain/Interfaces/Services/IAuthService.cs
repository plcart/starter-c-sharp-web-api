using Starter.Domain.Entities;

namespace Starter.Domain.Interfaces.Services
{
    public interface IAuthService:IServiceBase<User>
    {
        void Register(User model);
        User Login(string username, string password);
    }
}

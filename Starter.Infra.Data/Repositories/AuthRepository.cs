using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Repositories;
using Starter.Infra.Data.Helpers.Extensions;

namespace Starter.Infra.Data.Repositories
{
    public class AuthRepository : RepositoryBase<User>, IAuthRepository
    {
        public User Login(string username, string password)
        {
            var crypt = password.ToMD5();
            var user = Get(x => x.Username == username && x.Password == password);
            return user;
        }

        public void Register(User model)
        {
            model.Password = model.Password.ToMD5();
            Add(model);
        }
    }
}

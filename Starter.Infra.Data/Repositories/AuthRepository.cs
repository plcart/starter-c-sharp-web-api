using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Repositories;
using Starter.Infra.Data.Helpers.Extensions;
using System;
using System.Linq.Expressions;

namespace Starter.Infra.Data.Repositories
{
    public class AuthRepository : RepositoryBase<User>, IAuthRepository
    {
        public User Login(string username, string password)
        {
            var crypt = password.ToMD5();
            var user = Get(x => x.Username == username && x.Password == crypt, new Expression<Func<User, object>>[]
                {u=>u.Profile.Roles });
            return user;
        }

        public void Register(User model)
        {
            model.Password = model.Password.ToMD5();
            Add(model);
        }
    }
}

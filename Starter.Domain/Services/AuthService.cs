using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Repositories;
using Starter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Services
{
    public class AuthService : ServiceBase<User>, IAuthService
    {
        internal protected new IAuthRepository repository { get; }

        public AuthService(IAuthRepository repo)
            :base(repo)
        {
            repository = repo;
        }

        public User Login(string username, string password)
        {
            return repository.Login(username, password);
        }

        public void Register(User model)
        {
            repository.Register(model);
        }
    }
}

using CoreJwtExample.IRepository;
using CoreJwtExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreJwtExample.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<string> Delete(User ojb)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Get(int ojbId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByUsernamePassword(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> Gets()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Save(User ojb)
        {
            throw new System.NotImplementedException();
        }
    }
}

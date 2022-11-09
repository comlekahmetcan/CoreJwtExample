using CoreJwtExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreJwtExample.IRepository
{
    public interface IUserRepository
    {
        Task<User> Save(User ojb);
        Task<User> Get(int ojbId);
        Task<List<User>> Gets();
        Task<User> GetByUsernamePassword(User user);
        Task<string> Delete(User ojb);
    }
}

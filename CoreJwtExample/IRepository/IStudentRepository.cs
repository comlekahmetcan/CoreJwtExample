using CoreJwtExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreJwtExample.IRepository
{
    public interface IStudentRepository
    {
        Task<List<Student>> Gets1();
        Task<List<Student>> Gets2();

    }
}

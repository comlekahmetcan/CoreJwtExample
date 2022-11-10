using CoreJwtExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreJwtExample.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> Save(Student obj);
        Task<Student> Get(int studentId);
        Task<string> Delete(Student obj);
        Task<List<Student>> Gets1();
        Task<List<Student>> Gets2();

    }
}

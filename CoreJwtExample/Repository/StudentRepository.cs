using CoreJwtExample.Common;
using CoreJwtExample.IRepository;
using CoreJwtExample.Models;
using Dapper;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CoreJwtExample.Repository
{
    public class StudentRepository : IStudentRepository
    {
        string _connectionString = "";
        Student _oStudent = new Student();
        List<Student> _oStudents = new List<Student>();

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SchoolDB");
        }

        public async Task<string> Delete(Student obj)
        {
            string message = "";
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    await con.QueryAsync<Student>("SP_Student",
                        this.SetParameters(obj, (int)OperationType.Delete),
                        commandType: CommandType.StoredProcedure);
                    message = "Deleted";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<Student> Get(int studentId)
        {
            _oStudent = new Student();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var oStudents = await con.QueryAsync<Student>(String.Format(@"SELECT * FROM Student WHERE StudentId={0}", studentId));
                if (oStudents != null && oStudents.Count() > 0)
                {
                    _oStudent = oStudents.SingleOrDefault();
                }
            }
            return _oStudent;
        }

        public async Task<List<Student>> Gets1()
        {
            _oStudents = new List<Student>();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Students = await con.QueryAsync<Student>("SELECT * FROM Student");
                if (Students != null && Students.Count() > 0)
                {
                    _oStudents = Students.ToList();
                }
            }
            return _oStudents;
        }

        public async Task<List<Student>> Gets2()
        {
            _oStudents = new List<Student>();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Students = await con.QueryAsync<Student>("SELECT * FROM Student");
                if (Students != null && Students.Count() > 0)
                {
                    _oStudents = Students.ToList();
                }
            }
            return _oStudents;
        }

        public async Task<Student> Save(Student obj)
        {
            _oStudent = new Student();
            try
            {
                int operationType = Convert.ToInt32(obj.StudentId == 0 ? OperationType.Insert : OperationType.Update);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var oStudents = await con.QueryAsync<Student>("SP_Student",
                        this.SetParameters(obj, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (oStudents != null && oStudents.Count() > 0) _oStudent = oStudents.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _oStudent = new Student();
                _oStudent.Message = ex.Message;
            }
            return _oStudent;
        }

        private DynamicParameters SetParameters(Student oStudent, int nOperationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StudentId", oStudent.StudentId);
            parameters.Add("@Name", oStudent.Name);
            parameters.Add("@Roll", oStudent.Roll);
            parameters.Add("@OperationType", nOperationType);
            return parameters;
        }
    }
}

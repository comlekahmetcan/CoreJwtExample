using CoreJwtExample.Common;
using CoreJwtExample.IRepository;
using CoreJwtExample.Models;
using Dapper;
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
        public async Task<List<Student>> Gets1()
        {
            _oStudents = new List<Student>();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Students = await con.QueryAsync<Student>("SELECT * FROM User");
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
                var Students = await con.QueryAsync<Student>("SELECT * FROM User");
                if (Students != null && Students.Count() > 0)
                {
                    _oStudents = Students.ToList();
                }
            }
            return _oStudents;
        }
    }
}

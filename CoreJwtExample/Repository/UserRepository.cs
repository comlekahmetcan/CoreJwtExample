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
    public class UserRepository : IUserRepository
    {
        string _connectionString = "";
        User _oUser = new User();
        List<User> _oUsers = new List<User>();

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SchoolDB");
        }

        public async Task<string> Delete(User ojb)
        {
            string message = "";
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var Users = await con.QueryAsync<User>("SP_User",
                        this.SetParameters(ojb, (int)OperationType.Delete),
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

        public async Task<User> Get(int ojbId)
        {
            _oUser = new User();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Users = await con.QueryAsync<User>(String.Format(@"SELECT * FROM User WHERE UserId={0}", ojbId));
                if (Users != null && Users.Count() > 0)
                {
                    _oUser = Users.SingleOrDefault();
                }
            }
            return _oUser;
        }

        public async Task<User> GetByUsernamePassword(User user)
        {
            _oUser = new User();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sql = string.Format(@"SELECT * FROM [User] WHERE Username='{0}' AND Password='{1}'", user.Username, user.Password);
                var users = await con.QueryAsync<User>(sql);
                if (users != null && users.Count() > 0)
                {
                    _oUser = users.SingleOrDefault();
                }
            }
            return _oUser;
        }

        public async Task<List<User>> Gets()
        {
            _oUsers = new List<User>();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Users = await con.QueryAsync<User>("SELECT * FROM User");
                if (Users != null && Users.Count() > 0)
                {
                    _oUsers = Users.ToList();
                }
            }
            return _oUsers;
        }

        public async Task<User> Save(User ojb)
        {
            _oUser = new User();
            try
            {
                int operationType = Convert.ToInt32(ojb.UserId == 0 ? OperationType.Insert : OperationType.Update);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var Users = await con.QueryAsync<User>("SP_User",
                        this.SetParameters(ojb, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (Users != null && Users.Count() > 0) _oUser = Users.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _oUser = new User();
                _oUser.Message = ex.Message;
            }
            return _oUser;
        }

        private DynamicParameters SetParameters(User oUser, int nOperationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", oUser.UserId);
            parameters.Add("@Username", oUser.Username);
            parameters.Add("@Email", oUser.Email);
            parameters.Add("@Password", oUser.Password);
            parameters.Add("@OperationType", nOperationType);
            return parameters;
        }
    }
}

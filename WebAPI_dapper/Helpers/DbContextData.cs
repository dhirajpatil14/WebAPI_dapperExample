using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_dapper.Helpers
{
    public class DbContextData
    {
        private readonly IConfiguration _configuration;
        public DbContextData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("ConnStr"));
        }

        public async Task Init()
        {
            var conn = CreateConnection();

            await _initcustomer();

            async Task _initcustomer()
            {
                var sqlstr = "IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='Customer' and xtype='U') CREATE TABLE Customer(Id INT PRIMARY KEY IDENTITY(1,1),FirstName VARCHAR(100),LastName VARCHAR(100),Address VARCHAR(MAX),City VARCHAR(100),PhoneNumber VARCHAR(15));";

                await conn.ExecuteAsync(sqlstr);
            }
        }
    }
}

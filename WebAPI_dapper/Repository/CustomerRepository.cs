using Dapper;
using WebAPI_dapper.Entities;
using WebAPI_dapper.Helpers;

namespace WebAPI_dapper.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly DbContextData _context;

        public CustomerRepository(DbContextData context)
        {
            _context = context;
        }

        public async Task CustCreate(Customer customer)
        {
            using var connection = _context.CreateConnection();

            var sql = "INSERT INTO CUSTOMER(FirstName,LastName,Address,City,PhoneNumber)" + 
                "VALUES(@FirstName,@LastName,@Address,@City,@PhoneNumber)";

            await connection.ExecuteAsync(sql, customer);
        }

        public async Task CustDelete(int id)
        {
            using var connection = _context.CreateConnection();

            var sql = "DELETE FROM CUSTOMER WHERE Id=@id";

            await connection.ExecuteAsync(sql, new {id });
        }

        public async Task CustUpdate(int id, Customer customer)
        {
            using var connection = _context.CreateConnection();

            var sql = "UPDATE CUSTOMER SET FirstName=@FirstName,LastName=@LastName,Address=@Address," +
                "City=@City,PhoneNumber=@PhoneNumber WHERE Id=" + id;

            await connection.ExecuteAsync(sql, customer);
        }

        public async Task<Customer> FindById(int id)
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT * FROM CUSTOMER WHERE Id=@id";

            return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new {id });
        }

        public async Task<Customer> FindByName(string firstname)
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT * FROM CUSTOMER WHERE FirstName=@firstname";

            return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { firstname });
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT * FROM CUSTOMER WITH(NOLOCK)";

            return await connection.QueryAsync<Customer>(sql);
        }
    }
}

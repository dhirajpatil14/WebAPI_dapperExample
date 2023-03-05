using WebAPI_dapper.Entities;

namespace WebAPI_dapper.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> FindByName(string firstname);
        Task<Customer> FindById(int id);
        Task CustCreate(Customer customer); 
        Task CustUpdate(int id, Customer customer);
        Task CustDelete(int id);
    }
}

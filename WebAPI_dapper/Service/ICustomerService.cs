using WebAPI_dapper.Entities;
using WebAPI_dapper.Models;

namespace WebAPI_dapper.Service
{
    public interface ICustomerService
    {
        void CustomerCreate(CreateCustRequest custRequest);
        Task CustomerUpdate(int id, UpdateCustRequest custRequest);
        Task CustomerDelete(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
    }
}

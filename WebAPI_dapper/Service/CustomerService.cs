using AutoMapper;
using WebAPI_dapper.Entities;
using WebAPI_dapper.Helpers;
using WebAPI_dapper.Models;
using WebAPI_dapper.Repository;

namespace WebAPI_dapper.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public void CustomerCreate(CreateCustRequest custRequest)
        {
            var custExist = _customerRepository.FindByName(custRequest.FirstName);
            if(custExist != null)
            {
                throw new AppException("Customer Name: '" + custRequest.FirstName + "' Already Exist");
            }

            var customer = _mapper.Map<Customer>(custExist);
            
            _customerRepository.CustCreate(customer);
        }

        public async Task CustomerDelete(int id)
        {
            await _customerRepository.CustDelete(id);
        }

        public async Task CustomerUpdate(int id, UpdateCustRequest custRequest)
        {
            var customer = await _customerRepository.FindById(id);
            if(customer == null)
            {
                throw new AppException("Customer Not Found");
            }

            _mapper.Map(custRequest, customer);

            await _customerRepository.CustUpdate(id, customer);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerRepository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = _customerRepository.FindById(id);
            if (customer == null)
            {
                throw new AppException("Customer Not Found");
            }

            return await customer;
        }
    }
}

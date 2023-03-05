using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_dapper.Entities;
using WebAPI_dapper.Models;
using WebAPI_dapper.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_dapper.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customer = await _customerService.GetAll();
            return Ok(customer);
        }

        // GET api/<CustomerController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(CreateCustRequest createcustmodel)
        {
            _customerService.CustomerCreate(createcustmodel);

            return Ok(new { Message = "Customer Created" });
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCustRequest updatecustomer)
        {

            await _customerService.CustomerUpdate(id, updatecustomer);
            return Ok(new { Message = "Customer Updated" });
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerService.CustomerDelete(id);
            return Ok(new { Message = "Customer Deleted" });
        }
    }
}

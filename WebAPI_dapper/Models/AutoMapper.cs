using AutoMapper;
using WebAPI_dapper.Entities;

namespace WebAPI_dapper.Models
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<CreateCustRequest, Customer>();
            CreateMap<UpdateCustRequest, Customer>().ForAllMembers(m => m.Condition(
                (source, destination, prop) =>
                {
                    if(prop == null) return false;

                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;   
                    return true;
                }
                ));
        } 
    }
}

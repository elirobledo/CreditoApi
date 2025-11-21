using AutoMapper;
using CreditoService.Api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace CreditoService.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Credito, Credito>().ReverseMap();
        }
    }
}
using AgroVisitAPI.Models;
using AutoMapper;
using Core;

namespace AgroVisitAPI.Mappers
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteViewModel, Cliente>().ReverseMap();
        }
         
    }
}

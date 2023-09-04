using AgroVisitWeb.Models;
using AutoMapper;
using Core;

namespace AgroVisitWeb.Mappers
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteViewModel, Cliente>().ReverseMap();
        }
    }
}

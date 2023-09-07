using AgroVisitWeb.Models;
using AutoMapper;
using Core;

namespace AgroVisitWeb.Mappers
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile() 
        {
            CreateMap<ProjetoViewModel, Projeto>().ReverseMap();
        }
    }
}

using AgroVisitAPI.Models;
using AutoMapper;
using Core;

namespace AgroVisitAPI.Mappers
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile()
        {
            CreateMap<ProjetoViewModel, Projeto>().ReverseMap();
        }
    }
}

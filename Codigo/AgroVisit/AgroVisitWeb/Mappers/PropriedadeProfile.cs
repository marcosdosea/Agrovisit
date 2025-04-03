using AgroVisitWeb.Models;
using AutoMapper;
using Core;

namespace AgroVisitWeb.Mappers
{
    public class PropriedadeProfile : Profile
    {
        public PropriedadeProfile()
        {
            CreateMap<PropriedadeViewModel, Propriedade>().ReverseMap();
        }
    }
}
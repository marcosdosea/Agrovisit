using AgroVisitAPI.Models;
using AutoMapper;
using Core;

namespace AgroVisitAPI.Mappers
{
    public class VisitaProfile : Profile
    {
        public VisitaProfile()
        {
            CreateMap<VisitaViewModel, Visita>().ReverseMap();
        }
    }
}

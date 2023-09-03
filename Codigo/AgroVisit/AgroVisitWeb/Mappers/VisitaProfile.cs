using AgroVisitWeb.Models;
using AutoMapper;
using Core;

namespace AgroVisitWeb.Mappers
{
    public class VisitaProfile : Profile
    {
        public VisitaProfile() 
        {
            CreateMap<VisitaModel, Visita>().ReverseMap();
        }
    }
}

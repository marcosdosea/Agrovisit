using AgroVisitWeb.Models;
using AutoMapper;
using Core;

namespace AgroVisitWeb.Mappers
{
    public class IntervencaoProfile : Profile
    {
        public IntervencaoProfile()
        {
            CreateMap<IntervencaoViewModel, Intervencao>().ReverseMap();
        }
    }
}
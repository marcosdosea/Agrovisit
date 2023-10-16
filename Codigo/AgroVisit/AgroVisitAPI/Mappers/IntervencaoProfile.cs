using AgroVisitAPI.Models;
using AutoMapper;
using Core;

namespace AgroVisitAPI.Mappers
{
    public class IntervencaoProfile : Profile
    {
        public IntervencaoProfile()
        {
            CreateMap<IntervencaoViewModel, Intervencao>().ReverseMap();
        }
    }
}

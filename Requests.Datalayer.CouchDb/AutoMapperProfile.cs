using AutoMapper;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Dtos;

namespace Cmas.DataLayers.CouchDb.Requests
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Request, RequestDto>();
            CreateMap<RequestDto, Request>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src._id))
            .ForMember(
                dest => dest.RevId,
                opt => opt.MapFrom(src => src._rev));
        }
    }
}

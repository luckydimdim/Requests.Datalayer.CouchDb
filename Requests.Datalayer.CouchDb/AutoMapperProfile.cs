using AutoMapper;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using System;

namespace Cmas.DataLayers.CouchDb.Requests
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Request, RequestDto>()
                .ForMember(
                    dest => dest._id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<RequestDto, Request>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src._id))
                .ForMember(
                    dest => dest.RevId,
                    opt => opt.MapFrom(src => src._rev));

            CreateMap<RequestStatus, int>().ConvertUsing(src => (int) src);
            CreateMap<int, RequestStatus>()
                .ConvertUsing(src => (RequestStatus) Enum.Parse(typeof(RequestStatus), src.ToString()));
        }
    }
}
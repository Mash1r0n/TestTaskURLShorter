using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapperProfiles
{
    public class UrlDynamicMetadataMapper : Profile
    {
        public UrlDynamicMetadataMapper()
        {
            this.CreateMap<UrlDynamicMetadata, UrlDynamicMetadataDto>()
                .ForMember(dest => dest.ShortUrlId, opt => opt.MapFrom(src => src.ShortUrlId))
                .ForMember(dest => dest.Clicks, opt => opt.MapFrom(src => src.Clicks))
                .ForMember(dest => dest.LastAccessedAt, opt => opt.MapFrom(src => src.LastAccessedAt));

            this.CreateMap<UrlDynamicMetadataDto, UrlDynamicMetadata>()
                .ForMember(dest => dest.ShortUrlId, opt => opt.MapFrom(src => src.ShortUrlId))
                .ForMember(dest => dest.Clicks, opt => opt.MapFrom(src => src.Clicks))
                .ForMember(dest => dest.LastAccessedAt, opt => opt.MapFrom(src => src.LastAccessedAt));
        }
    }
}

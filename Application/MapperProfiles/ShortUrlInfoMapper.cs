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
    public class ShortUrlInfoMapper : Profile
    {
        public ShortUrlInfoMapper()
        {
            this.CreateMap<ShortUrl, ShortUrlInfoDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Clicks, opt => opt.MapFrom(src => src.DynamicMetadata.Clicks))
                .ForMember(dest => dest.LastAccessedAt, opt => opt.MapFrom(src => src.DynamicMetadata.LastAccessedAt));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RTLTestTask.ApiModels;

namespace RTLTest.Services.AutoMapper
{
    public class ModelMapper
    {
        private static readonly IMapper Mapper;

        public IConfigurationProvider Configuration => Mapper.ConfigurationProvider;

        static ModelMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TVShow, TVShowResponse>();

                cfg.CreateMap<Cast, CastPerson>();

                cfg.CreateMap<TVShowWithEmbed<CastEmbedList>, TVShowResponse>()
                    .ForMember(dest => dest.Casts,
                        opt => opt.MapFrom(src => Mapper.Map<List<CastPerson>>(src.Embed.Cast.Select(s => s.Person).ToList())));

                cfg.CreateMap<RTLTestTask.Models.Cast, CastPerson>();

                cfg.CreateMap<RTLTestTask.Models.TVShow, TVShowResponse>()
                    .ForMember(dest => dest.Casts,
                        opt => opt.MapFrom(src => Mapper.Map<List<CastPerson>>(src.ShowCasts.Select(s => s.Cast).ToList())));
            });

            Mapper = mapperConfig.CreateMapper();
        }

        public TVShowResponse GetShowResponse(TVShow from, List<Cast> casts)
        {
            var result = Mapper.Map<TVShowResponse>(from);
            result.Casts = casts.Select(s => s.Person).ToList();
            return result;
        }

        public TVShowResponse GetShowResponse(TVShowWithEmbed<CastEmbedList> from)
        {
            var result = Mapper.Map<TVShowResponse>(from);
            result.Casts = from.Embed.Cast.Select(s => s.Person).ToList();
            return result;
        }
    }
}

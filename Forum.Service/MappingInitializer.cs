using AutoMapper;
using Forum.Entities;
using Forum.Models;
using Forum.Models.Identity;

namespace Forum.Service
{
    public static class MappingInitializer
    {
        public static IMapper Initialize()
        {
            MapperConfiguration configuration = new(config =>

            {
                config.CreateMap<TopicEntity, TopicForCreatingDto>().ReverseMap();
                config.CreateMap<TopicEntity, TopicForUpdatingDto>().ReverseMap();
                config.CreateMap<TopicEntity, TopicForGettingDto>().ReverseMap();

                config.CreateMap<CommentEntity, CommentForGettingDto>().ReverseMap();
                config.CreateMap<CommentEntity, CommentForCreatingDto>().ReverseMap();
                config.CreateMap<CommentEntity, CommentForUpdatingDto>().ReverseMap();

                config.CreateMap<UserDto, ApplicationUser>().ReverseMap();
                config.CreateMap<RegistrationRequestDto, ApplicationUser>()
                      .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
                      .ForMember(destination => destination.NormalizedUserName, options => options.MapFrom(source => source.Email.ToUpper()))
                      .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                      .ForMember(destination => destination.NormalizedEmail, options => options.MapFrom(source => source.Email.ToUpper()))
                      .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNumber));

            });

            return configuration.CreateMapper();
        }
    }
}

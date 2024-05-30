using AutoMapper;
using Common.Application;
using Common.Application.DateUtil;
using Common.AspNetCore.Extensions;
using Common.Domain.ValueObjects;
using Identity.Core.Dto.User;
using Identity.Data.Entities;
using Identity.ViewModels.UserManager;

namespace Identity.Core.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Gender, opt => opt.MapFrom(c => c.Gender.ToDisplay(DisplayProperty.Name)))
                .ForMember(x=>x.Avatar,opt=>opt.MapFrom(x=>x.Avatar.GenerateStaticUrl()))
                .ReverseMap();

            CreateMap<User, GetUserForUpdateDto>()
                .ForMember(x => x.BirthDay, opt => opt.MapFrom(c => c.BirthDay.ToPersianDate()))

                .ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id));
            //  .ForMember(x=>x.Roles,opt=>opt.MapFrom(c=>c.Roles));


            CreateMap<GetUserForUpdateDto, UserEditViewModel>().ReverseMap();

            #region socialNetwork

            CreateMap<SocialNetwork, SocialNetworkDto>().ConvertUsing<SocialNetworkToSocialNetworkDtoAndViewModel>();
            CreateMap<SocialNetworkDto,SocialNetwork >().ConvertUsing<SocialNetworkDtoAndViewModelToSocialNetwork>();
            CreateMap<SocialNetwork, SocialNetworkViewModel>().ConvertUsing<SocialNetworkToSocialNetworkDtoAndViewModel>();
            CreateMap<SocialNetworkViewModel, SocialNetwork>().ConvertUsing<SocialNetworkDtoAndViewModelToSocialNetwork>();

            #endregion
            CreateMap<User, ManagmentUserCommandDto>()
                .ReverseMap();
            CreateMap<User, ManagedUserViewModel>()
                .ForMember(x => x.BirthDay, opt => opt.MapFrom(c => c.BirthDay.ToPersianDate()))
                .ReverseMap();

        }

    }
    //This is also easier to unit test if you have conversions that are not 100% automappable.
    public class SocialNetworkDtoAndViewModelToSocialNetwork : ITypeConverter<SocialNetworkDto, SocialNetwork>, ITypeConverter<SocialNetworkViewModel, SocialNetwork>
    {
        public SocialNetwork Convert(SocialNetworkDto source, SocialNetwork destination, ResolutionContext context)
        {
            if (source is null) return null;

            var destinationValue = new SocialNetwork(source.Instagram, source.Telegram, source.YouTube, source.WhatsApp,
                source.Linkdine, source.GitHub
                , source.Email, source.Discord);

            //do other magic you may want during conversion time

            return destinationValue;
        }

        public SocialNetwork Convert(SocialNetworkViewModel? source, SocialNetwork destination, ResolutionContext context)
        {
            if (source is null) return null;

            var destinationValue = new SocialNetwork(source.Instagram, source.Telegram, source.YouTube, source.WhatsApp,
                source.Linkdine, source.GitHub
                , source.Email, source.Discord);

            //do other magic you may want during conversion time

            return destinationValue;
        }
    }

    public class SocialNetworkToSocialNetworkDtoAndViewModel : ITypeConverter<SocialNetwork, SocialNetworkDto>, ITypeConverter<SocialNetwork, SocialNetworkViewModel>
    {
        public SocialNetworkDto Convert(SocialNetwork source, SocialNetworkDto destination, ResolutionContext context)
        {
            if (source is null) return null;
            var destinationValue = new SocialNetworkDto
            {
                Instagram = source.Instagram,
                Telegram = source.Telegram,
                YouTube = source.YouTube,
                WhatsApp = source.WhatsApp,
                Linkdine = source.Linkdin,
                GitHub = source.GitHub,
                Email = source.Email,
                Discord = source.Discord
            };

            return destinationValue;
        }

        public SocialNetworkViewModel Convert(SocialNetwork source, SocialNetworkViewModel destination, ResolutionContext context)
        {
            if (source is null) return null;
            var destinationValue = new SocialNetworkViewModel
            {
                Instagram = source.Instagram,
                Telegram = source.Telegram,
                YouTube = source.YouTube,
                WhatsApp = source.WhatsApp,
                Linkdine = source.Linkdin,
                GitHub = source.GitHub,
                Email = source.Email,
                Discord = source.Discord
            };

            return destinationValue;
        }
    }

}

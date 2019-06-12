using AdverticementManager.ViewModels;
using AdvertisementProfiles.VK.ResponseModels;
using AutoMapper;

namespace AdverticementManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VkAdProfilesResponseModel, VkAdProfilesViewModel>()
                .ForMember(dest => dest.AdProfiles, opt => opt.MapFrom(src => src.response));
            CreateMap<VkAdProfilesResponseModel.AdProfile, VkAdProfilesViewModel.AdProfile>()
                .ForMember(dest => dest.AccessRole, opt => opt.MapFrom(src => src.access_role))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.account_id))
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.account_status))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.account_type));
        }
    }
}

using AutoMapper;
using FM.Domain.Models;
using FM.External.API.Models;

namespace FM.Business.Profiles
{
    public class ExternalApiProfile: Profile
    {
        public ExternalApiProfile()
        {
            CreateMap<User, UserEntity>();
        }
    }
}

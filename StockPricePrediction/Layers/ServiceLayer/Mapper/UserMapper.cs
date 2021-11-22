using AutoMapper;
using DomainLayer;
using ServiceLayer.Models;

namespace ServiceLayer.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponseModel>().ForMember(user => user.Name,
                    user => user.MapFrom(user => $"{user.Firstname} {user.Lastname}"))
                .ForMember(user => user.Token,
                    user => user.MapFrom((user, context) => JwtConfig.GetToken(user)));
        }
    }
}
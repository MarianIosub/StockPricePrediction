using AutoMapper;
using DomainLayer;

namespace ServiceLayer.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponseModel>().ForMember(user => user.Name,
                    user => user.MapFrom(myUser => $"{myUser.Firstname} {myUser.Lastname}"))
                .ForMember(user => user.Token,
                    user => user.MapFrom((myUser) => JwtConfig.GetToken(myUser)));
        }
    }
}
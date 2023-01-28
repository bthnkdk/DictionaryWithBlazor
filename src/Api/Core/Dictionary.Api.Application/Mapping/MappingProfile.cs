using AutoMapper;
using Dictionary.Api.Domain.Models;
using Dictionary.Common.Models.CommandModels;
using Dictionary.Common.Models.QueriesModels;

namespace Dictionary.Api.Application.Mapping
{
    public class MappingProfilei : Profile
    {
        public MappingProfilei()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
        }
    }
}

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
            CreateMap<Entry, CreateEntryCommand>().ReverseMap();
            CreateMap<EntryComment, CreateEntryCommentCommand>().ReverseMap();
            CreateMap<Entry, GetEntriesViewModel>().ForMember(s => s.CommentCount, p => p.MapFrom(x => x.EntryComments.Count)).ReverseMap();
        }
    }
}

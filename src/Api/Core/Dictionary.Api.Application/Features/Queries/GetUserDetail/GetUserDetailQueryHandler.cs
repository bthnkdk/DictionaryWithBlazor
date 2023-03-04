using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Models.QueriesModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserDetailQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Domain.Models.User dbUser = null;

            if(request.UserId != Guid.Empty)
                dbUser = await userRepository.GetByIdAsync(request.UserId);
            else if(!string.IsNullOrEmpty(request.UserName))
                dbUser = await userRepository.GetSingleAsync(s => s.UserName == request.UserName);

            return mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}

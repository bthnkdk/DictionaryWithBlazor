using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Infrastructure.QueueFactory;
using Dictionary.Common.Models.CommandModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await userRepository.GetSingleAsync(s => s.EmailAddress == request.EmailAddress);

            if (isExist is not null)
                throw new DatabaseValidationException("User already exist");

            var dbUser = mapper.Map<Domain.Models.User>(request);
            var result = await userRepository.AddAsync(dbUser);

            if (result > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = request.EmailAddress
                };

                QueueFactory.SendMessageToExchange(exchangeName: Constant.UserExchangeName,
                                                   exchangeType: Constant.DefaultExchangeType,
                                                   queueName: Constant.UserEmailChangedQueueName,
                                                   obj: @event);
            }

            return dbUser.Id;
        }
    }
}

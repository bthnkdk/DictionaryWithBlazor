using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Infrastructure.QueueFactory;
using Dictionary.Common.Models.CommandModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            mapper.Map(request, dbUser);
            var result = await userRepository.UpdateAsync(dbUser);

            if (emailChanged && result > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = dbEmailAddress,
                    NewEmailAddress = request.EmailAddress
                };

                QueueFactory.SendMessageToExchange(exchangeName: Constant.UserExchangeName,
                                                   exchangeType: Constant.DefaultExchangeType,
                                                   queueName: Constant.UserEmailChangedQueueName,
                                                   obj: @event);

                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}

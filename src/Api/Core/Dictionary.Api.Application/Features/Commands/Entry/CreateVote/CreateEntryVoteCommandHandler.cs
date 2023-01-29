using Dictionary.Common;
using Dictionary.Common.Events.Entry;
using Dictionary.Common.Infrastructure.QueueFactory;
using Dictionary.Common.Models.CommandModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.VoteExchangeName,
                                            exchangeType: Constant.DefaultExchangeType,
                                            queueName: Constant.CreateEntryVoteQueueName,
                                            obj: new CreateEntryVoteEvent()
                                            {
                                                EntryId = request.EntryId,
                                                CreatedBy = request.CreatedBy
                                            });

            return await Task.FromResult(true);
        }
    }
}

using Dictionary.Common;
using Dictionary.Common.Events.Entry;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.VoteExchangeName,
                                              exchangeType: Constant.DefaultExchangeType,
                                              queueName: Constant.DeleteEntryVoteQueueName,
                                              obj: new DeleteEntryVoteEvent()
                                              {
                                                  EntryId = request.EntryId,
                                                  CreatedBy = request.UserId
                                              });

            return await Task.FromResult(true);
        }
    }
}

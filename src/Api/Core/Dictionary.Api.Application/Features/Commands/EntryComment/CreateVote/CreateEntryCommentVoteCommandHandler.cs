using Dictionary.Common;
using Dictionary.Common.Events.EntryComment;
using Dictionary.Common.Infrastructure.QueueFactory;
using Dictionary.Common.Models.CommandModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.VoteExchangeName,
                                               exchangeType: Constant.DefaultExchangeType,
                                               queueName: Constant.CreateEntryCommentVoteQueueName,
                                               obj: new CreateEntryCommentVoteEvent()
                                               {
                                                   EntryCommentId = request.EntryCommentId,
                                                   CreatedBy = request.CreatedBy,
                                                   VoteType = request.VoteType
                                               });

            return await Task.FromResult(true);
        }
    }
}

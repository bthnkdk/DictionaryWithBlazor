using Dictionary.Common;
using Dictionary.Common.Events.EntryComment;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.VoteExchangeName,
                                                exchangeType: Constant.DefaultExchangeType,
                                                queueName: Constant.DeleteEntryCommentVoteQueueName,
                                                obj: new DeleteEntryCommentVoteEvent()
                                                {
                                                    EntryCommentId = request.EntryCommentId,
                                                    CreatedBy = request.UserId
                                                });

            return await Task.FromResult(true);
        }
    }
}

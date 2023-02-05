using Dictionary.Common;
using Dictionary.Common.Events.EntryComment;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {

        public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.FavExchangeName,
                                                exchangeType: Constant.DefaultExchangeType,
                                                queueName: Constant.DeleteEntryCommentFavQueueName,
                                                obj: new DeleteEntryCommentFavEvent()
                                                {
                                                    EntryCommentId = request.EntryCommentId,
                                                    CreatedBy = request.UserId
                                                });

            return await Task.FromResult(true);
        }
    }
}

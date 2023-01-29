using Dictionary.Common;
using Dictionary.Common.Events.EntryComment;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {

            QueueFactory.SendMessageToExchange(exchangeName: Constant.FavExchangeName,
                                               exchangeType: Constant.DefaultExchangeType,
                                               queueName: Constant.CreateEntryCommentFavQueueName,
                                               obj: new CreateEntryCommentFavEvent()
                                               {
                                                   EntryCommentId = request.EntryCommentId,
                                                   CreatedBy = request.UserId
                                               });

            return await Task.FromResult(true);
        }
    }
}

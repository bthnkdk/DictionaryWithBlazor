using Dictionary.Common;
using Dictionary.Common.Events.Entry;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.FavExchangeName,
                                               exchangeType: Constant.DefaultExchangeType,
                                               queueName: Constant.CreateEntryCommentFavQueueName,
                                               obj: new CreateEntryFavEvent()
                                               {
                                                   EntryId = request.EntryId.Value,
                                                   CreatedBy = request.UserId.Value
                                               });

            return await Task.FromResult(true);
        }
    }
}

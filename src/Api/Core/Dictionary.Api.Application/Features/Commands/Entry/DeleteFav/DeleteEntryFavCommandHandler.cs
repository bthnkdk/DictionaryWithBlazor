using Dictionary.Common;
using Dictionary.Common.Events.Entry;
using Dictionary.Common.Infrastructure.QueueFactory;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constant.FavExchangeName,
                                             exchangeType: Constant.DefaultExchangeType,
                                             queueName: Constant.DeleteEntryFavQueueName,
                                             obj: new DeleteEntryFavEvent()
                                             {
                                                 EntryId = request.EntryId,
                                                 CreatedBy = request.UserId
                                             });

            return await Task.FromResult(true);
        }
    }
}

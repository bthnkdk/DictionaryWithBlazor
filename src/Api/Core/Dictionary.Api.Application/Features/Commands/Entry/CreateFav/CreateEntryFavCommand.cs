﻿using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommand : IRequest<bool>
    {
        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }

        public CreateEntryFavCommand(Guid? userId, Guid? entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}

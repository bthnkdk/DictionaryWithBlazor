using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Models.CommandModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.Entry.Create
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public CreateEntryCommandHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            this.mapper = mapper;
            this.entryRepository = entryRepository;
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var dbEntry = mapper.Map<Domain.Models.Entry>(request);
            await entryRepository.AddAsync(dbEntry);

            return dbEntry.Id;
        }
    }
}

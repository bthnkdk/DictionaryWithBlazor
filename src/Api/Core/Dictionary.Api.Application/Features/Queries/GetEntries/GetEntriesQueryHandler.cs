using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Models.QueriesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if (request.TodaysEntries)
            {
                query = query.Where(s => s.CreateDate >= DateTime.Now.Date)
                             .Where(s => s.CreateDate <= DateTime.Now.AddDays(1).Date);
            }

            query = query.Include(s => s.EntryComments)
                   .OrderBy(s => Guid.NewGuid())
                   .Take(request.Count);

            return await query.ProjectTo<GetEntriesViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}

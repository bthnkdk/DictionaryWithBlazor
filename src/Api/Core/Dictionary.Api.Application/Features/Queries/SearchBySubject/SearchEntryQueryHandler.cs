using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Models.QueriesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            var result = entryRepository.Get(s => EF.Functions.Like(s.Subject, $"{request.SearchText}%"))
                                        .Select(s => new SearchEntryViewModel()
                                        {
                                            Id = s.Id,
                                            Subject = s.Subject
                                        });

            return await result.ToListAsync(cancellationToken);
        }
    }
}

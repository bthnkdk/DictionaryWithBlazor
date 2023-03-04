using Dictionary.Api.Application.Features.Commands.Entry.DeleteVote;
using Dictionary.Api.Application.Features.Commands.EntryComment.DeleteVote;
using Dictionary.Common.Models.CommandModels;
using Dictionary.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.WebApi.Controllers
{
    public class VoteController : BaseController
    {
        private readonly IMediator mediator;

        public VoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryVoteCommand(entryId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryVoteCommand(UserId.Value, entryId));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await mediator.Send(new DeleteEntryCommentVoteCommand(UserId.Value, entryCommentId));

            return Ok(result);
        }
    }
}

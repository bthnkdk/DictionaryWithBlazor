using MediatR;

namespace Dictionary.Common.Models.CommandModels
{
    public class CreateEntryCommand : IRequest<Guid>
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid? CreatedById { get; set; }

        public CreateEntryCommand(Guid? createdById, string content, string subject)
        {
            CreatedById = createdById;
            Content = content;
            Subject = subject;
        }
    }
}

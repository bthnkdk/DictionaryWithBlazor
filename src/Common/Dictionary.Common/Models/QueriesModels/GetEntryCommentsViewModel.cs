namespace Dictionary.Common.Models.QueriesModels
{
    public class GetEntryCommentsViewModel : BaseFooterRateFavouritedViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}

namespace Dictionary.Common.Models.QueriesModels
{
    public class GetUserEntriesDetailViewModel : BaseFooterFavouritedViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}

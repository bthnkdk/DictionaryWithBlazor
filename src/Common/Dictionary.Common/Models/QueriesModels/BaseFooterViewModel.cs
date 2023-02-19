using Dictionary.Common.ViewModels;

namespace Dictionary.Common.Models.QueriesModels
{
    public class BaseFooterRateViewModel
    {
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavouritedViewModel
    {
        public bool IsFavourited { get; set; }
        public int FavouritedCount { get; set; }
    }

    public class BaseFooterRateFavouritedViewModel : BaseFooterFavouritedViewModel
    {
        public VoteType VoteType { get; set; }
    }
}

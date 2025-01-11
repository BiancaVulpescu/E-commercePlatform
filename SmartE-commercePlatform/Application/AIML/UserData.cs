namespace Application.AIML
{
    public class UserData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> RecentSearches { get; set; }
        public List<Guid> RecentlyViewedProductIds { get; set; }
    }
}
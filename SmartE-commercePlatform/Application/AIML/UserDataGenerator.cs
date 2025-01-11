namespace Application.AIML
{
    public static class UserDataGenerator
    {
        public static List<UserData> GenerateUserData()
        {
           namespace Application.AIML
    {
        public static class UserDataGenerator
        {
            public static List<UserData> GenerateUserData()
            {
                return new List<UserData>
            {
                new UserData
                {
                    Username = "user1",
                    Email = "user1@example.com",
                    RecentSearches = new List<string> { "smartphone", "wireless headphones" },
                    RecentlyViewedProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
                },
                new UserData
                {
                    Username = "user2",
                    Email = "user2@example.com",
                    RecentSearches = new List<string> { "waterproof jacket", "hiking boots" },
                    RecentlyViewedProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
                },
                new UserData
                {
                    Username = "user3",
                    Email = "user3@example.com",
                    RecentSearches = new List<string> { "ebook", "book covers" },
                    RecentlyViewedProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
                },
                new UserData
                {
                    Username = "user4",
                    Email = "user4@example.com",
                    RecentSearches = new List<string> { "gaming laptop", "external SSD" },
                    RecentlyViewedProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
                },
                new UserData
                {
                    Username = "user5",
                    Email = "user5@example.com",
                    RecentSearches = new List<string> { "kitchen knives", "non-stick skillet" },
                    RecentlyViewedProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
                }
            };
            }
        }
    }

}
    }
}
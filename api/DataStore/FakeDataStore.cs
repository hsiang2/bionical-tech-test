using Bionical.TechTest.Api.Models;
using Bogus;

namespace Bionical.TechTest.Api.DataStore;

public static class FakeDataStore
{
    private static readonly List<User> FakeUsers = [];
    public static IEnumerable<User> GetUsers()
    {
        if (FakeUsers.Count == 0)
        {
            Randomizer.Seed = new Random(1234);
        
            var userFaker = new Faker<User>()
                .CustomInstantiator(f => new User(
                    Guid.NewGuid(),
                    f.Name.FirstName(),
                    f.Name.LastName(),
                    f.Internet.Email(),
                    f.PickRandom<Gender>(),
                    DateOnly.FromDateTime(f.Date.PastOffset(80, DateTimeOffset.Now.AddYears(-18)).Date),
                    f.Random.Bool(),
                    DateTime.UtcNow
                ));
        
            FakeUsers.AddRange(userFaker.Generate(40));
        }
        
        return FakeUsers;
    }
}
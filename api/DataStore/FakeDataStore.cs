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
    public static User? UpdateUser(Guid id, User updated)
    {
        var index = FakeUsers.FindIndex(user => user.Id == id);
        if (index == -1) return null;
        
        var existing = FakeUsers[index];

        var newUser = existing with
        {
            FirstName = updated.FirstName,
            LastName = updated.LastName,
            Email = updated.Email,
            Gender = updated.Gender,
            DateOfBirth = updated.DateOfBirth,
            Status = updated.Status,
            LastUpdated = DateTime.UtcNow
        };

        FakeUsers[index] = newUser;

        return newUser;
    }
}
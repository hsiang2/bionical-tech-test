namespace Bionical.TechTest.Api.Models;

public record User(Guid Id, string FirstName, string LastName, string Email, Gender Gender, DateOnly DateOfBirth, bool Status, DateTime LastUpdated)
{
    
}
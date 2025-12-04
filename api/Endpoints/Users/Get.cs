using Bionical.TechTest.Api.Infrastructure;
using Bionical.TechTest.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bionical.TechTest.Api.Endpoints.Users;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id:guid}", Endpoint);
    }

    private Results<Ok<Response>, NotFound> Endpoint(Guid id)
    {
        var user = DataStore.FakeDataStore.GetUsers().FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Response(user.Id, user.FirstName, user.LastName, user.Email, user.Gender,
            user.DateOfBirth, user.Status, user.LastUpdated));
    }
    
    private record Response(Guid Id, string FirstName, string LastName, string Email, Gender Gender, DateOnly DateOfBirth, bool Status, DateTime LastUpdated);
}
using Bionical.TechTest.Api.Infrastructure;
using Bionical.TechTest.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bionical.TechTest.Api.Endpoints.Users;

public class List : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", Endpoint);
    }
    
    private static Ok<Response> Endpoint()
    {
        return TypedResults.Ok(new Response(DataStore.FakeDataStore.GetUsers()));
    }
    
    private record Response(IEnumerable<User> Users);
}
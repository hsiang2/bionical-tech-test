using System.Net.Mail;
using Bionical.TechTest.Api.Infrastructure;
using Bionical.TechTest.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bionical.TechTest.Api.Endpoints.Users;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}", Endpoint);
    }

    private static Results<Ok<Response>, BadRequest<ErrorResponse>, NotFound> Endpoint(Guid id, User request)
    {
        var errors = Validate(request);

        if (errors.Count > 0)
        {
            return TypedResults.BadRequest(new ErrorResponse(errors));
        }

        var updated = DataStore.FakeDataStore.UpdateUser(id, request);

        if(updated is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Response(
            updated.Id,
            updated.FirstName,
            updated.LastName,
            updated.Email,
            updated.Gender,
            updated.DateOfBirth,
            updated.Status,
            updated.LastUpdated
        ));
    }

    private static List <string> Validate(User user)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(user.FirstName))
            errors.Add("FirstName is required.");
        
        if (string.IsNullOrWhiteSpace(user.LastName))
            errors.Add("LastName is required.");

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors.Add("Email is required.");
        } else
        {
            try
            {
                var _ = new MailAddress(user.Email);
            }
            catch
            {
                errors.Add("Email format is invalid.");
            }
        }

        if (user.DateOfBirth == default)
            errors.Add("DateOfBirth is required.");

        return errors;
    }

    private record Response(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        Gender Gender,
        DateOnly DateOfBirth,
        bool Status,
        DateTime LastUpdated
    );

    private record ErrorResponse(IEnumerable<string> Errors);
}
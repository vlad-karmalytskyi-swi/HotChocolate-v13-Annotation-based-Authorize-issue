using HotChocolate.Authorization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "HotChocolate v13 Annotation-based [Authorize] issue");
app.MapGraphQL();
app.Run();

[Authorize]
public class User
{
    public string Name { get; set; }

    [Authorize]
    public string Address { get; set; }
}

public class Filter
{
    public string FilterName { get; set; }
}

public class Query
{
    public User GetUser() =>
        new User
        {
            Name = "User1",
            Address = "None"
        };

    public Filter GetFilter() => new Filter { FilterName = "Filter 1" };
}

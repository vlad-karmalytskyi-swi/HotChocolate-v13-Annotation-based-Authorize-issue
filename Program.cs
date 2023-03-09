using HotChocolate.Authorization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();
app.MapGet("/", () => "HotChocolate v13 Annotation-based [Authorize] issue");
app.MapGraphQL();
app.Run();

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<Query>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
    }
}

//public class UserType : ObjectType<User>
//{
//    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
//    {
//        descriptor.Authorize();
//        descriptor.Field(f => f.Age).Authorize();
//    }
//}

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
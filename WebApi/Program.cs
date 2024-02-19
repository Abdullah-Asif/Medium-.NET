using Bogus;
using Medium.Application;
using Medium.Domain.Entities;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterWebApiServices(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.RegisterUserEndpoints();
app.RegisterBlogEndpoints();
app.RegisterAuthEndpoint();
app.UseAuthentication();
app.UseAuthorization();


app.MapPost("fill", async (ApplicationDbContext dbContext) =>
{
    using var transaction = await dbContext.Database.BeginTransactionAsync();
    try
    {
        await dbContext.Users.ExecuteDeleteAsync();
        await dbContext.Blogs.ExecuteDeleteAsync();

        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, _ => new Guid())
            .RuleFor(u => u.UserName, f => f.Person.UserName)
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Address, f => f.Address.FullAddress());
        
        var users = userFaker.Generate(100);

        var blogFaker = new Faker<Blog>().
            RuleFor(b => b.Id, _ => new Guid())
            .RuleFor(b => b.Title, f => f.Lorem.Sentence())
            .RuleFor(b => b.Content, f => f.Lorem.Sentences())
            .RuleFor(b => b.User, f => f.PickRandom(users));

        var blogs = blogFaker.Generate(100);
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.Blogs.AddRangeAsync(blogs);

        await dbContext.SaveChangesAsync();

        await dbContext.Database.CommitTransactionAsync();
    }
    catch
    {
        await dbContext.Database.RollbackTransactionAsync();
        throw;
    }
});

app.Run();
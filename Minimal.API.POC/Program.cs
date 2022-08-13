using Minimal.API.POC.Data;
using Minimal.API.POC.Model;
using Minimal.API.POC.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/people", (AppDbContext context) =>
{
    var people = context.People;
    return people is null ? Results.NotFound() : Results.Ok(people);
});

app.MapPost("/people", (AppDbContext context, Person model) =>
{
    if (model is null)
        return Results.BadRequest();

    PersonValidator validator = new();
    var validation = validator.Validate(model);

    if (!validation.IsValid)
    {
        return Results.BadRequest(validation.Errors);
    }

    context.People!.Add(model);
    context.SaveChanges();

    return Results.Created($"/people/{model.Id}", model);
});

app.Run();
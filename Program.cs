using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FitDb>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "FitAPI";
    config.Title = "FitAPI v1";
    config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "FitAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/athletes", async (FitDb db) =>
    await db.Athletes.ToListAsync());

app.MapGet("/athlete/{id}", async (int id, FitDb db) =>
    await db.Athletes.FindAsync(id)
        is Athlete athlete
            ? Results.Ok(athlete)
            : Results.NotFound());

app.MapPost("/athlete", async (Athlete athlete, FitDb db) =>
{
    db.Athletes.Add(athlete);
    await db.SaveChangesAsync();

    return Results.Created($"/athlete/{athlete.Id}", athlete);
});

app.MapPut("/athlete/{id}", async (int id, Athlete inputAthlete, FitDb db) =>
{
    var athlete = await db.Athletes.FindAsync(id);

    if (athlete is null) return Results.NotFound();

    athlete.Name = inputAthlete.Name;
    athlete.DateOfBirth = inputAthlete.DateOfBirth;
    athlete.Weight = inputAthlete.Weight;
    athlete.Height = inputAthlete.Height;
    athlete.Equipments = inputAthlete.Equipments;
    athlete.Activities = inputAthlete.Activities;
    athlete.Workouts = inputAthlete.Workouts;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/athlete/{id}", async (int id, FitDb db) =>
{
    if (await db.Athletes.FindAsync(id) is Athlete athlete)
    {
        db.Athletes.Remove(athlete);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
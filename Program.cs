using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FitDb>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(PostgresRepository<>));
builder.Services.AddScoped<AthleteService>();
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

app.MapGet("/athletes", async (AthleteService service) =>
{
    var athletes = await service.GetAllAsync();
    return Results.Ok(athletes);
});

app.MapGet("/athletes/{id}", async (int id, AthleteService service) =>
{
    var athlete = await service.GetByIdAsync(id);
    return athlete is not null ? Results.Ok(athlete) : Results.NotFound();
});

app.MapPost("/athletes", async (Athlete athlete, FitDb db) =>
{
    db.Athletes.Add(athlete);
    await db.SaveChangesAsync();

    return Results.Created($"/athlete/{athlete.Id}", athlete);
});

app.MapPut("/athletes/{id}", async (int id, Athlete inputAthlete, FitDb db) =>
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

app.MapDelete("/athletes/{id}", async (int id, FitDb db) =>
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
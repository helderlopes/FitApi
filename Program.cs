using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AthleteDb>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/athletes", async (AthleteDb db) =>
    await db.Athletes.ToListAsync());

app.MapGet("/athlete/{id}", async (int id, AthleteDb db) =>
    await db.Athletes.FindAsync(id)
        is Athlete athlete
            ? Results.Ok(athlete)
            : Results.NotFound());

app.MapPost("/athlete", async (Athlete athlete, AthleteDb db) =>
{
    db.Athletes.Add(athlete);
    await db.SaveChangesAsync();

    return Results.Created($"/athlete/{athlete.Id}", athlete);
});

app.MapPut("/athlete/{id}", async (int id, Athlete inputAthlete, AthleteDb db) =>
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

app.MapDelete("/athlete/{id}", async (int id, AthleteDb db) =>
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
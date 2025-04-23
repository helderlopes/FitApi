using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

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
    app.UseSwaggerUi();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FitDb>();
    db.Database.Migrate();
}

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

app.MapPost("/athletes", async (AthleteCreateDto dto, AthleteService service) =>
{
    var created = await service.CreateAsync(dto);
    return Results.Created($"/athletes/{created.Id}", created);
});

app.MapPut("/athletes/{id}", async (int id, AthleteUpdateDto dto, AthleteService service) =>
{
    var success = await service.UpdateAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/athletes/{id}", async (int id, AthleteService service) =>
{
    var success = await service.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/equipments", async (EquipmentService service) =>
{
    var equipments = await service.GetAllAsync();
    return Results.Ok(equipments);
});
/*
app.MapGet("/equipments/{id}", async (int id, EquipmentService service) =>
{
    var equipment = await service.GetByIdAsync(id);
    return equipment is not null ? Results.Ok(equipment) : Results.NotFound();
});

app.MapPost("/equipments", async (EquipmentCreateDto dto, EquipmentService service) =>
{
    var created = await service.CreateAsync(dto);
    return Results.Created($"/equipments/{created.Id}", created);
});

app.MapPut("/equipments/{id}", async (int id, EquipmentUpdateDto dto, EquipmentService service) =>
{
    var success = await service.UpdateAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/equipments/{id}", async (int id, EquipmentService service) =>
{
    var success = await service.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});
*/
app.Run();
using ViaPadelClub_Kim_DCA1.Core.Application;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Dispatching;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Mappings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string[] allowedOrigins = (
    Environment.GetEnvironmentVariable("VIAPADELCLUB_ALLOWED_ORIGINS")
    ?? "http://localhost:3000;http://localhost:5173;http://localhost:61511")
    .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IObjectMapper>(_ =>
{
    ObjectMapper mapper = new();
    mapper.Register(new GetUpcomingDailySchedulesRequestMapping());
    mapper.Register(new UpcomingDailySchedulesAnswerMapping());
    return mapper;
});

string? connectionString =
    builder.Configuration.GetConnectionString("Postgres")
    ?? Environment.GetEnvironmentVariable("VIAPADELCLUB_POSTGRES_CONNECTION_STRING");

if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddPostgresDmPersistence(connectionString);
    builder.Services.AddApplicationCommands();
    builder.Services.AddPostgresQueries(connectionString);
}
else
{
    builder.Services.AddScoped<ICommandDispatcher, UnavailableCommandDispatcher>();
    builder.Services.AddScoped<IQueryDispatcher, UnavailableQueryDispatcher>();
}

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;

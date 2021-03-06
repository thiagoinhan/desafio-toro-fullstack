using MongoDB.Driver;
using Toro.Accounting.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200");
        builder.WithOrigins("http://129.153.22.240");
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

if (!app.Environment.IsEnvironment("test"))
{
    app.Services.GetRequiredService<IMongoDatabase>().SeedData();
}

app.MapControllers();
app.UseCors();
app.Run();

public partial class Program { }
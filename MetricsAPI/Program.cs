using MetricsAPI.Configuration;
using MetricsAPI.DataLayer;
using MetricsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMetricsHelper, MetricsHelper>();
builder.Services.AddDbContext<MetricsDbContext>(
    options =>
    {
        options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["MetricsConnection"], o => o.SetPostgresVersion(9, 6));
    });

// when running locally enable CORS. replace the port numnbers for API and UI eg http://testhost:3000
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7130", "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

//Mapster configuration
builder.Services.AddMapster();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

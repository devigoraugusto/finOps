using Microsoft.EntityFrameworkCore;
using finOps.Infra.Persistence.EF;
using StackExchange.Redis;
using finOps.Infra.Cache.Repositories;
using finOps.Application.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicione a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanyValidator>())
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<InvoiceValidator>());
builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConfiguration = Environment.GetEnvironmentVariable("REDIS_URL")
        ?? builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(redisConfiguration);
});

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICartCacheRepository, CartCacheRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS antes dos endpoints
app.UseCors("AllowAll");

// Adicione o Swagger aqui
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase("/api/v1/finops");
app.UseRouting();
app.UseHttpsRedirection();

await app.RunAsync();


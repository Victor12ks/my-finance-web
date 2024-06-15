using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Application.Applications;
using MyFinanceWeb.Application.Services;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Validations;
using MyFinanceWeb.Infra.Contexts;
using MyFinanceWeb.Infra.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyFinanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número máximo de tentativas
            maxRetryDelay: TimeSpan.FromSeconds(30), // Atraso máximo entre tentativas
            errorNumbersToAdd: null) // Lista opcional de números de erro adicionais para incluir no comportamento de repetição
    ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<PlanoContaValidator>());

builder.Services.AddScoped<IPlanoContaRepository, PlanoContaRepository>();
builder.Services.AddScoped<IPlanoContaApplication, PlanoContaApplication>();
builder.Services.AddScoped<IPlanoContaService, PlanoContaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

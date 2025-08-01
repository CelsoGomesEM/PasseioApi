using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Passeio.Api.Configuration;
using Passeio.Api.Extensions;
using Passeio.Data.Context;

var builder = WebApplication.CreateBuilder(args);

/// 1. Configuração explícita com LoggerFactory
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

var mapperConfigExpression = new MapperConfigurationExpression();
mapperConfigExpression.AddProfile<MappingProfile>();

// Cria a configuração com LoggerFactory
var mapperConfig = new MapperConfiguration(mapperConfigExpression, loggerFactory);

// Valida os mapeamentos (opcional)
mapperConfig.AssertConfigurationIsValid();

// Cria e registra o IMapper
builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

// Add services to the container.

builder.Services.AddDbContext<ApiDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder =>
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.ResolveDependecies();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfig();
builder.Services.AddLoggingConfig();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseAuthentication();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHsts();

app.UseHttpsRedirection();

app.UseCors("Development");

app.UseAuthorization();

app.MapControllers();

app.UseLoggingConfiguration();

app.Run();

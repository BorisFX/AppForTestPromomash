using AppForTest.API;
using AppForTest.Domain.Entities;
using AppForTest.Handlers.Commands;
using AppForTest.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddDbContext<AppForTestDbContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(UserCommandHandler).GetTypeInfo().Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Auto migration and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

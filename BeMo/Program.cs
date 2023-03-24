using BeMo.Data;
using BeMo.Models;
using BeMo.Repositories;
using BeMo.Repositories.Interfaces;
using BeMo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBeMoContext>(options => options.GetRequiredService<BeMoContext>());
builder.Services.AddDbContext<BeMoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BeMoContext") ??
                      throw new InvalidOperationException("Connection string 'BeMoContext' not found.")));

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Challenge>, ChallengeRepository>();
builder.Services.AddScoped<IRepository<Activity>, ActivityRepository>();

// Add services to the container.
builder.Services.AddHostedService<RefreshStravaTokensRepeatingService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

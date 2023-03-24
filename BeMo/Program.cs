using BeMo.Data;
using BeMo.Extensions;
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

builder.Services.AddDependencyServices(builder);

// Add services to the container.
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

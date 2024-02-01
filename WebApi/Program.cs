using System.Net;
using Medium.Application.Interfaces;
using Medium.Infrastructure;
using Medium.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), 
        b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.RegisterUserEndpoints();

app.Run();
using AutoMapper;
using FluentValidation.AspNetCore;
using Fragments.Domain.Extensions;
using Fragments.Domain.Profiles;
using Fragments.Domain.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDomainDataServices();
builder.Services.AddFluentValidation(config => config
    .RegisterValidatorsFromAssemblyContaining<UserService>());


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(options => options.WithOrigins("http://localhost:3000")
                              .AllowAnyMethod()
                              .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

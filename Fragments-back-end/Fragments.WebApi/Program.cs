using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using FluentValidation.AspNetCore;
using Fragments.Domain.Extensions;
using Fragments.Domain.Hubs;
using Fragments.Domain.Profiles;
using Fragments.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Fragments.Domain.Services.Implementation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddDomainDataServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDI();
builder.Services.AddFluentValidation(config => config
    .RegisterValidatorsFromAssemblyContaining<Fragments.Domain.Services.UserService>());

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfiles(new List<Profile> { new UserProfile(), new NotificationsProfile() });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddJwtValidation(builder);
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(options => options.AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapHub<NotificationsHub>("/Notifications");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

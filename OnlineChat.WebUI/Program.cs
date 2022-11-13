using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineChat.Core.Commands.Users;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Interfaces.Repositories;
using OnlineChat.Core.Interfaces.Services;
using OnlineChat.Core.Queries;
using OnlineChat.Infrastructure.Data;
using OnlineChat.Infrastructure.Data.Repositories;
using OnlineChat.WebUI.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Custom Services
builder.Services.AddMediatR(typeof(RegistrationCommand));

// AddInfrastructureDependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();

// AddCoreDependencies - some services

// AddWebUiDependencies

// Context
builder.Services.AddDbContext<OnlineChatContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey( // TODO: Config identity secret Configuration["Identity:SecurityKey"]
                                Encoding.UTF8.GetBytes("aksdokjafbkjasbfjabojsfbda"))
            };
        }
     );

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<OnlineChatContext>()
    .AddDefaultTokenProviders();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "iTechArtPizzaDelivery.WebUI", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = @"JWT Authorization header using the Bearer scheme. 
                                      Enter 'Bearer' [space] and then your token in the text input below.
                                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new string[] { }
                    }
                });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineChat.WebUI.Hubs;
using Shared;
using System.Text;
using Domain.Entities;
using Hellang.Middleware.ProblemDetails;
using EntityFramework.SqlServer;
using OnlineChat.WebUI.Extensions;
using Serilog;
using StackExchange.Redis;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddProblemDetails(setup =>
    {
        setup.IncludeExceptionDetails = (context, exception) =>
            builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
    });


    builder.Services.AddControllers();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console());

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region Custom Services

    ConfigurationManager configuration = builder.Configuration;
    builder.Services.Configure<AuthenticationConfiguration>(configuration.GetSection("Authentication"));
    builder.Services.Configure<IdentityConfiguration>(configuration.GetSection("Identity"));
    builder.Services.Configure<AzureBlobConfiguration>(configuration.GetSection("AzureBlob"));

    builder.Services.AddSignalR();

    builder.Services.AddAutoMappers();

    builder.Services.AddClerk(configuration.GetConnectionString("ClerkConnection"));

    builder.Services.AddMediatR();

    builder.Services.AddRepositories();

    builder.Services.AddAzureBlobStorage(configuration.GetConnectionString("AzureBlobConnection"));

    builder.Services.AddMassTransit();

    builder.Services.AddValidators();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddServices();

    builder.Services.AddSqlServerDbContext(configuration.GetConnectionString("DefaultConnection"));

    builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
    });

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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Authentication:SecurityKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        // if request to hub
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/direct")))
                        {
                            // then get token from request
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            }
        );

    builder.Services.AddIdentityCore<User>()
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<OnlineChatContext>()
        .AddDefaultTokenProviders();


    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineChat", Version = "v1" });
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
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
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

    app.UseProblemDetails();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHub<DirectMessageHub>("/direct");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
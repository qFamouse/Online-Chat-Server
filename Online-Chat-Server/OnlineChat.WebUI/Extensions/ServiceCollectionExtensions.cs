using Application.Interfaces.Mappers;
using Application.Mappers;
using Application.Services.Abstractions;
using Application.Services.Implementations;
using Azure.Storage.Blobs;
using Repositories;
using System.Reflection;
using MassTransit;
using Application.Validators;
using FluentValidation;
using MediatR;
using OnlineChat.WebUI.Services;
using Repositories.Abstractions;
using Services.Interfaces;

namespace OnlineChat.WebUI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoMappers(this IServiceCollection services)
        {
            services.AddSingleton<IDirectMessageMapper, DirectMessageMapper>();
            services.AddSingleton<IAttachmentMapper, AttachmentMapper>();
            services.AddSingleton<IUserMapper, UserMapper>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDirectMessageRepository, DirectMessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IConversationMessagesRepository, ConversationMessagesRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        }

        public static void AddAzureBlobStorage(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(x => new BlobServiceClient(connectionString));
            services.AddScoped<IBlobService, BlobService>();
        }

        public static void AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                // By default, sagas are in-memory, but should be changed to a durable
                // saga repository.
                x.SetInMemorySagaRepositoryProvider();

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ValidationBehavior<,>)));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<HubConnectionService>();
        }
    }
}

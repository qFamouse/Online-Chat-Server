﻿using Application.Mappers;
using Azure.Storage.Blobs;
using Repositories;
using System.Reflection;
using MassTransit;
using Application.Validators;
using FluentValidation;
using MediatR;
using Repositories.Abstractions;
using Application.Mappers.Abstractions;
using NuGet.Clerk.DependencyInjection;
using Services;
using Services.Abstractions;
using Application.CQRS.Commands.Users;

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
            services.AddSingleton<IHubConnectionsService, HubConnectionsService>();
        }

        public static void AddClerk(this IServiceCollection services, string baseAddress)
        {
            services.AddClerk(cfg =>
            {
                cfg.BaseAddress = new Uri(baseAddress);
            });
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(SignUpUserCommand).Assembly);
            });
        }
    }
}

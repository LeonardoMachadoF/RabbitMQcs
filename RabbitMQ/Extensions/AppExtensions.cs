using MassTransit;
using RabbitMQ.Bus;
using System.Runtime.CompilerServices;

namespace RabbitMQ.Extensions
{
    internal static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services)
        {
            services.AddTransient<IPublishBus, PublishBus>();
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();

                busConfigurator.UsingRabbitMq((ctx, cfg) =>
                { 

                    cfg.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                    
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}

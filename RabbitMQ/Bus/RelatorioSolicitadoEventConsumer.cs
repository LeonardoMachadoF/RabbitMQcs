using MassTransit;
using RabbitMQ.Relatorios;

namespace RabbitMQ.Bus
{
    internal sealed class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
    {
        private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;

        public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
        {
            _logger.LogInformation("Processando Relatorio Id:{Id}, Nome: {Nome}", context.Message.Id, context.Message.Name);

            await Task.Delay(10000);

            var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == context.Message.Id);

            if(relatorio != null)
            {
                relatorio.Status = "Completado!";
                relatorio.ProcessedTime = DateTime.Now;
            }

            _logger.LogInformation("Relatorio Processado Id:{Id}, Nome: {Nome}", context.Message.Id, context.Message.Name);
        }
    }
}

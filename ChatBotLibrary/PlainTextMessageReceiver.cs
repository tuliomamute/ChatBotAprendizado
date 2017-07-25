using System;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using System.Diagnostics;
using Business;

namespace ChatBotLibrary
{
    public class PlainTextMessageReceiver : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public PlainTextMessageReceiver(IMessagingHubSender sender)
        {
            _sender = sender;

            DeputadosController dep = new DeputadosController();
            var lista = dep.RetornaListaDeputados();
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"From: {message.From} \tContent: {message.Content}");
            await _sender.SendMessageAsync("Aguarde, estamos pesquisando a respeito do deputado escolhido :)", message.From, cancellationToken);
            Thread.Sleep(5000);

            await _sender.SendMessageAsync("Só mais um pouquinho e estará pronto! :)", message.From, cancellationToken);
            Thread.Sleep(5000);

            await _sender.SendMessageAsync("Pronto! Os resultados serão mostrados em breve! :)", message.From, cancellationToken);

        }
    }
}

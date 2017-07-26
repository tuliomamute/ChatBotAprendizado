using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using Models;
using Takenet.MessagingHub.Client;

namespace ChatBotLibrary
{
    public class ExpenseCongressManDataReceive : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public ExpenseCongressManDataReceive(IMessagingHubSender sender)
        {
            _sender = sender;
        }
        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Implementar busca das despesas por Mês
            if (Opcoes.DeputadoEscolhido == null)
            {
                await _sender.SendMessageAsync("Problema na Armazenagem do Deputado Escolhido!", message.From, cancellationToken);
                return;
            }

            await _sender.SendMessageAsync($"Estou Calculando os Gastos do Deputado {Opcoes.DeputadoEscolhido.nomeParlamentar}. Aguarde um pouquinho :)", message.From, cancellationToken);

        }
    }
}

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
using System.Net.Http;
using Business;

namespace ChatBotLibrary
{
    public class ExpenseCongressManDataReceive : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;
        private GastosController despesas;
        public ExpenseCongressManDataReceive(IMessagingHubSender sender)
        {
            _sender = sender;
            despesas = new GastosController();
        }
        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Opcoes.Instance.DeputadoEscolhido == null)
            {
                await _sender.SendMessageAsync("Problema na Armazenagem do Deputado Escolhido!", message.From, cancellationToken);
                return;
            }

            await _sender.SendMessageAsync($"Estou Calculando os Gastos do Deputado {Opcoes.Instance.DeputadoEscolhido.nomeParlamentar}. Aguarde um pouquinho :)", message.From, cancellationToken);

            string DespesasDeputado = await despesas.BuscarDespesas();

            await _sender.SendMessageAsync("Opa! Terminei os calculos e já irei exibi-los!", message.From, cancellationToken);
            Thread.Sleep(2000);

            await _sender.SendMessageAsync(DespesasDeputado, message.From, cancellationToken);

        }
    }
}

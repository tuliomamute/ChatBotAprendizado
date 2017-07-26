using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using Takenet.MessagingHub.Client;
using System.Diagnostics;

namespace ChatBotLibrary
{
    public class DataMessageReceiver : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public DataMessageReceiver(IMessagingHubSender sender)
        {
            _sender = sender;
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"From: {message.From} \tContent: {message.Content}");
            await _sender.SendMessageAsync($"A data de hoje é: {DateTime.Now.Date.ToString("dd/MM/yyyy")}", message.From, cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;

namespace ChatBotLibrary
{
    public class ExpenseCongressManDataReceive : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public ExpenseCongressManDataReceive(IMessagingHubSender sender)
        {
            _sender = sender;
        }
        public Task ReceiveAsync(Message envelope, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}

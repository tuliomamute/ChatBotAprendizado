using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using Takenet.MessagingHub.Client.Extensions.Directory;
using Takenet.MessagingHub.Client.Extensions.Resource;
using Lime.Messaging.Contents;
using Takenet.MessagingHub.Client;

namespace ChatBotLibrary
{
    public class TestReceiver : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public TestReceiver(IMessagingHubSender sender)
        {
            _sender = sender;
        }

        public async Task ReceiveAsync(Message envelope, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Pegando Informações do usuário
            DirectoryExtension d = new DirectoryExtension(_sender);
            var x = d.GetDirectoryAccountAsync(envelope.From.ToIdentity(), cancellationToken).Result;



            //Armazenando recursos
            ResourceExtension r = new ResourceExtension(_sender);
            var retorno = r.SetAsync<PlainText>("123649", new PlainText { Text = "Votações do Deputado" });
            PlainText teste = null;

            if (!retorno.IsCompleted)
                teste = r.GetAsync<PlainText>("123649").Result;

            await _sender.SendMessageAsync(teste.Text, envelope.From, cancellationToken);
        }
    }
}

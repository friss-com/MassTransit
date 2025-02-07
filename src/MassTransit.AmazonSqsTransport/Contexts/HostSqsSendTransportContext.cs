namespace MassTransit.AmazonSqsTransport.Contexts
{
    using Context;
    using GreenPipes;
    using Transport;


    public class HostSqsSendTransportContext :
        BaseSendTransportContext,
        SqsSendTransportContext
    {
        public HostSqsSendTransportContext(IClientContextSupervisor clientContextSupervisor, IPipe<ClientContext> configureTopologyPipe, string entityName,
            bool copyHeadersToMessageAttributes, ILogContext logContext)
            : base(logContext)
        {
            ClientContextSupervisor = clientContextSupervisor;
            ConfigureTopologyPipe = configureTopologyPipe;
            EntityName = entityName;
            CopyHeadersToMessageAttributes = copyHeadersToMessageAttributes;
        }

        public IPipe<ClientContext> ConfigureTopologyPipe { get; }
        public string EntityName { get; }
        public bool CopyHeadersToMessageAttributes { get; }
        public IClientContextSupervisor ClientContextSupervisor { get; }
    }
}

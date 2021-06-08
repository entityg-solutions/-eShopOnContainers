using System;

namespace eShopOnContainers.Events.Core
{
    public class IntegrationEvent
    {
        public IntegrationEvent(string correlationId)
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
            CorrelationId = correlationId;
        }

        public string Id { get; private set; }

        public string CorrelationId { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string Originate { get; set; }

        public string MachineName { get; set; }

    }
}

using System;

namespace eShopOnContainers.Events.Base
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string Originate { get; set; }

        public string MachineName { get; set; }
    }
}

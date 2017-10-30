using System;

namespace Kafka.Demo.Core.Domain
{
    public  class Behavior
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

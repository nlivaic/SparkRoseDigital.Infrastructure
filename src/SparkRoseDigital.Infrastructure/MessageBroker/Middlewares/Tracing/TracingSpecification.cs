using System.Collections.Generic;
using System.Linq;
using GreenPipes;
using MassTransit;

namespace SparkRoseDigital.Infrastructure.MessageBroker.Middlewares.Tracing
{
    public class TracingSpecification : IPipeSpecification<ConsumeContext>
    {
        public IEnumerable<ValidationResult> Validate() => Enumerable.Empty<ValidationResult>();

        public void Apply(IPipeBuilder<ConsumeContext> builder) => builder.AddFilter(new TracingFilter());
    }
}

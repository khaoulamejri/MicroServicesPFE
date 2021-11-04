using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace ParamGeneral.API.Consumer
{
    public class CompanyConsumedConsumer : IConsumer<CompanyConsumed>
    {
        public Task Consume(ConsumeContext<CompanyConsumed> context)
        {
            throw new NotImplementedException();
        }
    }
}

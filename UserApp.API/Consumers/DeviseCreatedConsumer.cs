using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;
using static Event.Contracts;

namespace UserApp.API.Consumers
{
    public class DeviseCreatedConsumer : IConsumer<DeviseCreated>
    {
        public readonly IDeviseServices deviseService;

        public DeviseCreatedConsumer(IDeviseServices deviseService)
        {
            this.deviseService = deviseService;
        }

        public async Task Consume(ConsumeContext<DeviseCreated> context)
        {
            var message = context.Message;



            var devise
               = new Devise
               {
                //   Id = message.DeviseIDConsumed,
                   Code = message.Code,
                   Intitule = message.Intitule,
                   Decimal = message.Decimal,
                   ExchangeRate = message.ExchangeRate,
                   DateModif = message.DateModif
               };

            deviseService.Edit(devise);
        }
    }
}

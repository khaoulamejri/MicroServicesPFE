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
    public class PayCreatedConsumer : IConsumer<PayCreated>
    {
        public readonly IPaysServices consumerService;

        public PayCreatedConsumer(IPaysServices consumerService)
        {
            this.consumerService = consumerService;
        }

        public async Task Consume(ConsumeContext<PayCreated> context)
        {
            var message = context.Message;



            var pay = new Pays
            {
                //  Id = message.PaysIdConsumed,
                Code = message.Code,
                Intitule = message.Intitule,
                DeviseCode = message.DeviseCode



            };

            consumerService.Create(pay);
        }
    }
}
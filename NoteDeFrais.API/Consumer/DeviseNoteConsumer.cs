using MassTransit;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace NoteDeFrais.API.Consumer
{
    public class DeviseNoteConsumer : IConsumer<DeviseNote>
    {
        public readonly IDeviseServices consumerService;

        public DeviseNoteConsumer(IDeviseServices consumerService)
        {
            this.consumerService = consumerService;
        }
        public async Task Consume(ConsumeContext<DeviseNote> context)
        {
            var message = context.Message;



            var devise = new Devise
            {
              //Id = message.DeviseIdConsumed,
                Decimal = message.Decimal

            };

            consumerService.Create(devise);
        }
    }
}

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
    public class PaysCreatedConsumer : IConsumer<PaysUserCreated>
    {
        public readonly IPaysServices consumerService;

        public PaysCreatedConsumer(IPaysServices consumerService)
        {
            this.consumerService = consumerService;
        }
        public async Task Consume(ConsumeContext<PaysUserCreated> context)
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

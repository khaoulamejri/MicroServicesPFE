using Conge.Domain.Entities;
using Conge.Services.Iservices;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace Conge.API.Consumers
{
    public class UniteConsumedConsumer : IConsumer<UniteConsumed>
    {
        public readonly IUniteService consumerService;

        public UniteConsumedConsumer(IUniteService consumerService)
        {
            this.consumerService = consumerService;
        }

        public async Task Consume(ConsumeContext<UniteConsumed> context)
        {
            var message = context.Message;

            var emp = new Unite
            {
                //  Id = message.IdEmployee,
                UserCreat = message.UserCreat,
                UserModif = message.UserModif,
                DateCreat = message.DateCreat,
                DateModif = message.DateModif,
                companyID = message.companyID,
                Code = message.Code,
                Intitule = message.Intitule
            };

            consumerService.Create(emp);
        }
    }
}
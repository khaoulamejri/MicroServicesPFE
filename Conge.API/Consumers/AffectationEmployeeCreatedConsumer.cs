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
    public class AffectationEmployeeCreatedConsumer : IConsumer<AffectationEmployeeCreated>
    {
        public readonly IAffectationEmployeeServices consumerService;

        public AffectationEmployeeCreatedConsumer(IAffectationEmployeeServices consumerService)
        {
            this.consumerService = consumerService;
        }

        public async Task Consume(ConsumeContext<AffectationEmployeeCreated> context)
        {
            var message = context.Message;

            var emp = new AffectationEmployee
            {
                //  Id = message.IdEmployee,
                UserCreat = message.UserCreat,
                UserModif = message.UserModif,
                DateCreat = message.DateCreat,
                DateModif = message.DateModif,
                companyID = message.companyID,
                 EmployeeID = message.EmployeeID,
                DateDebut = message.DateDebut,
                DateFin = message.DateFin,
                PositionID = message.PositionID,
                Principal = message.Principal,

            };

            consumerService.Create(emp);
        }
    }
}
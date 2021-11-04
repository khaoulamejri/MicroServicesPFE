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
    public class CompanyGetIDConsumer : IConsumer<CompanyGetID>
    {
        private readonly ITypeCongeServices typeCongeServices;

        public CompanyGetIDConsumer(ITypeCongeServices typeCongeServices)
        {
            this.typeCongeServices = typeCongeServices;
        }

        public async Task Consume(ConsumeContext<CompanyGetID> context)
        {
            //var message = context.Message;



            //var company = new Company
            //{
            //    Id = message.IdCompany,
              
            //};

            //typeCongeServices.CreateCompany(company);
        }
    }
}

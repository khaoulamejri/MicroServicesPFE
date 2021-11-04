//using MassTransit;
//using NoteDeFrais.Domain.Entities;
//using NoteDeFrais.Services.IServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using static Event.Contracts;

//namespace NoteDeFrais.API.Consumer
//{
//    public class DemandeCongeNoteCreatedConsumer : IConsumer<DemandeCongeNoteCreated>
//    {
//        public readonly IConsumerServices consumerService;

//        public DemandeCongeNoteCreatedConsumer(IConsumerServices consumerService)
//        {
//            this.consumerService = consumerService;
//        }

//        public async Task Consume(ConsumeContext<DemandeCongeNoteCreated> context)
//        {
//            var message = context.Message;



//            var demandeConge= new DemandeConge
//            {
//                Id = message.IdDemandeConge,
//                DateDebutConge = message.DateDebutConge,
//                DateRepriseConge = message.DateRepriseConge,
//                Statut = message.Statut,
//                EmployeeIDConsumed = message.EmployeeIDConsumed

//            };

//            consumerService.CreateDemande(demandeConge);
//        }
//    }
//}

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
//    public class WFDocumentCreatedConsumer : IConsumer<WFDocumentCreated>
//    {
//        public readonly IConsumerServices consumerService;

//        public WFDocumentCreatedConsumer(IConsumerServices consumerService)
//        {
//            this.consumerService = consumerService;
//        }
//        public async Task Consume(ConsumeContext<WFDocumentCreated> context)
//        {

//            var message = context.Message;



//            var wFDocument = new WFDocument
//            {
//                Id = message.IdWFDocument,
//                UserCreat = message.UserCreat,
//                UserModif = message.UserModif,
//                DateCreat = message.DateCreat,
//                DateModif = message.DateModif,
//                companyID = message.companyID,
//                TypeDocument = message.TypeDocument,
//                Finished = message.Finished,
//                AffectedToId = message.AffectedToId,
//                DocumentId = message.DocumentId



//            };

//            consumerService.CreateWFDocument(wFDocument);
//        }
//    }
//}

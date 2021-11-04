﻿using Conge.Domain.Entities;
using Conge.Services.Iservices;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace Conge.API.Consumers
{
    public class EmployeeCongeCreatedConsumer : IConsumer<EmployeeCongeCreated>
    {
        public readonly IEmployeeServices consumerService;

        public EmployeeCongeCreatedConsumer(IEmployeeServices consumerService)
        {
            this.consumerService = consumerService;
        }
        public async Task Consume(ConsumeContext<EmployeeCongeCreated> context)
        {
            var message = context.Message;



            var emp = new Employee
            {
                //  Id = message.IdEmployee,
                UserCreat = message.UserCreat,
                UserModif = message.UserModif,
                DateCreat = message.DateCreat,
                DateModif = message.DateModif,
                companyID = message.companyID,
                NumeroPersonne = message.NumeroPersonne,
                Nom = message.Nom,
                Prenom = message.Prenom,
                DateNaissance = message.DateNaissance,
                CIN = message.CIN,
                DeliveryDateCin = message.DeliveryDateCin,
                PlaceCin = message.PlaceCin,
                PassportNumber = message.PassportNumber,
                ValidityDateRP = message.ValidityDateRP,
                RecruitementDate = message.RecruitementDate,
                TitularizationDate = message.TitularizationDate,
                Tel = message.Tel,
                TelGSM = message.TelGSM,
                Mail = message.Mail,
                Langue = message.Langue,
                Adresse = message.Adresse,
                Ville = message.Ville,
                CodePostal = message.CodePostal,

                User = message.User,
                PlanDroitCongeIDConsumed = message.PlanDroitCongeIDConsumed,
                RegimeTravailID = message.RegimeTravailID,
                ConsultantExterne = message.ConsultantExterne


            };

            consumerService.Create(emp);
        }
    }
}
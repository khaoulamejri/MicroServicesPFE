using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
    //piController]
    public class EmployeeController : ControllerBase
    {

        private readonly IAffectationEmployeeServices AffectationEmployeeServices;
        private readonly IEmployeeServices employeeServices;
        private readonly IParamGenerauxServices paramGenerauxServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRegimeTravailServices regimeTravailServices;


        public EmployeeController(IAffectationEmployeeServices affectationEmployeeServices, IEmployeeServices emp, IParamGenerauxServices param, IHttpContextAccessor httpContextAccessor, IPublishEndpoint publishEndpoint, IRegimeTravailServices reg)
        {
            regimeTravailServices = reg;

            AffectationEmployeeServices = affectationEmployeeServices;
         
            employeeServices = emp;
            //     userServices = user;
            paramGenerauxServices = param;
           // _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _publishEndpoint = publishEndpoint;
        }
        //[ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var listEmployee = employeeServices.GetAllEmployee();
            return StatusCode(200, listEmployee);
        }

        //[ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpPost, Route("Post")]
        public async Task<IActionResult> POSTAsync([FromBody] EmployeeViewModel Employee, string dateNaissance, string validityDateRP, string deliveryDateCin)
        {
            var empl = new Employee();
            empl = Employee.MappingAddEmployee(Employee, empl);
            empl.DateNaissance = dateNaissance.Equals("null") ? empl.DateNaissance : DateTime.ParseExact(dateNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            empl.ValidityDateRP = validityDateRP.Equals("null") ? empl.ValidityDateRP : DateTime.ParseExact(validityDateRP, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            empl.DeliveryDateCin = deliveryDateCin.Equals("null") ? empl.DeliveryDateCin : DateTime.ParseExact(deliveryDateCin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }

            var employees = employeeServices.GetAllEmployees();
            try
            {
                var alert = employeeServices.CheckUnicityEmployeeParameterCreate(employees, Employee);
                if (alert != "")
                {
                    return StatusCode(400, alert);
                }
                else
                {
                 var po = regimeTravailServices.GetRegimTravailByID(empl.RegimeTravailID);
                   empl.RegimeTravail = po;

                    employeeServices.Create(empl);
               await _publishEndpoint.Publish(new EmployeeNoteCreated(empl.Id, empl.UserCreat, empl.UserModif, empl.DateCreat, empl.DateModif, empl.companyID, empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne));
                    
                 await _publishEndpoint.Publish(new EmployeeCongeCreated(empl.Id, empl.UserCreat, empl.UserModif, empl.DateCreat, empl.DateModif, empl.companyID, empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne));
                 await _publishEndpoint.Publish(new EmployeeUserCreated(empl.Id, empl.UserCreat, empl.UserModif, empl.DateCreat, empl.DateModif, empl.companyID, empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne));

                    if (empl == null)
                    {
                        return StatusCode(400, "FailCreateEmployee");
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, empl);
        }
        [HttpPost, Route("POSTEmp")]
        public  IActionResult POSTEmp([FromBody] Employee Employee)
        {
           var empl = new Employee();
            //empl = Employee.MappingAddEmployee(Employee, empl);
            //empl.DateNaissance = dateNaissance.Equals("null") ? empl.DateNaissance : DateTime.ParseExact(dateNaissance, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //empl.ValidityDateRP = validityDateRP.Equals("null") ? empl.ValidityDateRP : DateTime.ParseExact(validityDateRP, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //empl.DeliveryDateCin = deliveryDateCin.Equals("null") ? empl.DeliveryDateCin : DateTime.ParseExact(deliveryDateCin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //if (!ModelState.IsValid)
            //{
            //    return StatusCode(400, "Model_Invalid");
            //}

            var employees = employeeServices.GetAllEmployees();
            try
            {
                var alert = employeeServices.CheckUnicityEmployeeParameterCreat(employees, Employee);
                if (alert != "")
                {
                    return StatusCode(400, alert);
                }
                else
                {

                  empl = employeeServices.Create(Employee);
             //     await _publishEndpoint.Publish(new EmployeeUserCreated(Employee.Id, Employee.UserCreat, Employee.UserModif, Employee.DateCreat, Employee.DateModif, Employee.companyID, Employee.NumeroPersonne, Employee.Nom, Employee.Prenom, Employee.DateNaissance, Employee.CIN, Employee.DeliveryDateCin, Employee.PlaceCin, Employee.PassportNumber, Employee.ValidityDateRP, Employee.RecruitementDate, Employee.TitularizationDate, Employee.Tel, Employee.TelGSM, Employee.Mail, Employee.Langue, Employee.Adresse, Employee.Ville, Employee.CodePostal, Employee.Photo, Employee.User, Employee.PlanDroitCongeIDConsumed, Employee.RegimeTravailID, Employee.ConsultantExterne));

                    if (empl == null)
                    {
                        return StatusCode(400, "FailCreateEmployee");
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, empl);
        }
        [HttpPost, Route("POSTT")]
        public async Task<IActionResult> POSTTAsync([FromBody] Employee Employee)
        {
            var empl = new Employee();
        //      DateModif = Employee.DateModif,
        //   UserModif = Employee.UserModif,
        //   NumeroPersonne = Employee.NumeroPersonne,
        //   Nom = Employee.Nom,
        //   Prenom = Employee.Prenom,
        //   DateNaissance = Employee.DateNaissance,
        //   CIN = Employee.CIN,
        //   Gender = Employee.Gender,
        //  DeliveryDateCin = Employee.DeliveryDateCin,
        //   PlaceCin = Employee.PlaceCin,
        //  RecruitementDate = Employee.RecruitementDate,
        //   Tel = Employee.Tel,
        //   TelGSM = Employee.TelGSM,
        //  Mail = Employee.Mail,
        //  Adresse = Employee.Adresse,
        //    Ville = Employee.Ville,
        //  CodePostal = Employee.CodePostal,
        //   PlanDroitCongeIDConsumed = Employee.PlanDroitCongeIDConsumed,
        //   RegimeTravailID = Employee.RegimeTravailID,
        //    MaritalStatus = Employee.MaritalStatus,
        //    PassportNumber = Employee.PassportNumber,
        //   ValidityDateRP = Employee.ValidityDateRP,
        //    TitularizationDate = Employee.TitularizationDate,


        //   Langue = Employee.Langue,

        //  Photo = Employee.Photo,
        //  User = Employee.User,
        //   ConsultantExterne = Employee.ConsultantExterne



        //};
            try
            {
                if (Employee == null)
                {
                    return StatusCode(400, "FailPaysObject");
                }

                if (employeeServices.CheckUnicityEmploye(Employee.Nom))
                {

                    empl = employeeServices.Create(Employee);
                  await _publishEndpoint.Publish(new EmployeeUserCreated(Employee.Id, Employee.UserCreat, Employee.UserModif, Employee.DateCreat, Employee.DateModif, Employee.companyID, Employee.NumeroPersonne, Employee.Nom, Employee.Prenom, Employee.DateNaissance, Employee.CIN, Employee.DeliveryDateCin, Employee.PlaceCin, Employee.PassportNumber, Employee.ValidityDateRP, Employee.RecruitementDate, Employee.TitularizationDate, Employee.Tel, Employee.TelGSM, Employee.Mail, Employee.Langue, Employee.Adresse, Employee.Ville, Employee.CodePostal, Employee.User, Employee.PlanDroitCongeIDConsumed, Employee.RegimeTravailID, Employee.ConsultantExterne));

                    if (empl == null)
                    {
                        return StatusCode(400, "FailCreateemp");
                    }
                }
                else
                {
                    return StatusCode(400, "CodeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, empl);
        }
        // [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpPut, Route("Put")]
        public IActionResult Put([FromBody] EmployeeViewModel Employee, int id, string dateNaissance, string validityDateRP, string deliveryDateCin,
         string recruitementDate, string titularizationDate)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }
            var empl = new Employee();
            empl = Employee.MappingAddEmployee(Employee, empl);
            empl.Id = id;
            empl.DateNaissance = dateNaissance.Equals("null") ? empl.DateNaissance : DateTime.ParseExact(dateNaissance, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            empl.ValidityDateRP = validityDateRP.Equals("null") ? empl.ValidityDateRP : DateTime.ParseExact(validityDateRP, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            empl.DeliveryDateCin = deliveryDateCin.Equals("null") ? empl.DeliveryDateCin : DateTime.ParseExact(deliveryDateCin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            empl.RecruitementDate = recruitementDate.Equals("null") ? empl.RecruitementDate : DateTime.ParseExact(recruitementDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            empl.TitularizationDate = titularizationDate.Equals("null") ? empl.TitularizationDate : DateTime.ParseExact(titularizationDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var affect = new List<AffectationEmployee>();
            affect = AffectationEmployeeServices.GetAffectationEmployeeByEmployeeIdcmp(id);
            if (affect.Exists(a => a.DateDebut < empl.RecruitementDate))
            {
                return StatusCode(400, "RecrutementControl");
            }
            var employees = employeeServices.GetAllEmployees();
            if (Employee != null)
            {
                try
                {
                    if (employeeServices.GetEmployeeByID(Employee.Id) != null)
                    {
                        var alert = employeeServices.CheckUnicityEmployeeParameterUpdate(employees, Employee);
                        if (alert != null)
                        {
                            return StatusCode(400, alert);
                        }

                        else
                        {
                           // var userToFind = userServices.GetUserByUserName(Employee.User);
                            employeeServices.Edit(empl);
                         //   _publishEndpoint.Publish(new EmployeeUserUpdated(empl.Id, empl.UserCreat, empl.UserModif, empl.DateCreat, empl.DateModif, empl.companyID, empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.Gender, empl.MaritalStatus, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.Photo, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne));

                            //if (userToFind != null)
                            //{
                            //    userToFind.Email = Employee.Mail;
                            //    userToFind.NormalizedEmail = Employee.Mail;
                            //    userServices.Edit(userToFind);

                            //}
                            if (empl == null)
                            {
                                return StatusCode(400, "Problème lors de modification d'employée ");
                            }
                        }
                    }
                    else
                    {
                        return StatusCode(400, "EmployeeExit");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }
            }
            return StatusCode(200, empl);
        }
        //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var empl = employeeServices.Delete(id);
                _publishEndpoint.Publish(new EmployeeUserDeleted(id));
                if (empl == null)
                {
                    return StatusCode(400, "FailDeleteEmployee");
                }
                return StatusCode(200, empl);

            }
            catch (Exception e)
            {
                return StatusCode(400, "FailDeleteEmployee");
            }
        }


    }
}

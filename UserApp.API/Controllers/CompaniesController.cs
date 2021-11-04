using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.API.Common;
using UserApp.Data;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;
using static Event.Contracts;
using static UserApp.API.Common.DTO;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICompanyServices companyServices;
        private readonly IPaysServices _paysServices;
        private readonly IUserServices UserServices;
        private readonly IDeviseServices deviseServices;
        private readonly IUserRolesServices UserRolesServices;
        private readonly IUserRoleCompaniesServices UserRoleCompaniesServices;
        private readonly IEmployeeServices _employeeServices;
        //  private readonly IDelegationService _delegationService;
        private readonly IPublishEndpoint _publishEndpoint;


        public CompaniesController(IConfiguration configuration, ApplicationDbContext Context, SignInManager<ApplicationUser> signInManager,
            ICompanyServices serv, IUserServices uservices, IUserRolesServices rolesServices, IPublishEndpoint publishEndpoint, 
            IUserRoleCompaniesServices userRoleCompaniesServices, IPaysServices paysServices, IEmployeeServices employeeServices, IDeviseServices devise)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            companyServices = serv;
            UserServices = uservices;
            UserRolesServices = rolesServices;
            UserRoleCompaniesServices = userRoleCompaniesServices;
            _paysServices = paysServices;
            _employeeServices = employeeServices;
            deviseServices = devise;
            _publishEndpoint = publishEndpoint;
            // _delegationService = delegationService;


        }

        [ProducesResponseType(200)]
      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllCompany")]
        public IActionResult GetAllCompany()
        {
            var listCompany = companyServices.GetAllCompany();
            return StatusCode(200, listCompany);
        }

        [ProducesResponseType(200)]
   //     [ClaimRequirement("Auth", "Authenticated")]
        //[HttpGet, Route("GetAllCompanyByUser")]
        //public IActionResult GetAllCompanyByUser(string id)
        //{
        //    var Companies = new List<Company>();
        //    var CompaniesDeleg = new List<Company>();
        //    var CompaniesDeleg2 = new List<Company>();

        //    var comp = new List<int>();
        //    var UserName = UserServices.GetUserName();
        //    DateTime date = DateTime.UtcNow;
        //    bool hasDelegation = false;
        //    List<Delegation> empoyeeDelegations = new List<Delegation>();

        //    if (id == "admin")
        //    {
        //        var user = UserServices.GetUserByUserName("admin");
        //        Companies = companyServices.GetCompaniesByUser(user.Id);
        //    }
        //    else
        //    {
        //        var user = UserServices.GetUserByUserName(UserName);
        //        var empl = _employeeServices.GetEmployeeByUserName(UserName);
        //        if (empl != null)
        //            hasDelegation = _delegationService.hasDelegation(empl.Id);
        //        if (hasDelegation)
        //        {
        //            empoyeeDelegations = _delegationService.GetDelegationByRemplaçantId(empl.Id).Where(d => d.DateDebut <= date && date < d.DateFin).ToList();
        //            foreach (var item in empoyeeDelegations)
        //            {
        //                var emp = _employeeServices.GetEmployeeByID(item.Employee);

        //                var usr = UserServices.GetUserByUserName(emp.User);
        //                CompaniesDeleg = companyServices.GetCompaniesByUser(usr.Id).Distinct().ToList();
        //                if (CompaniesDeleg.Count() != 0)
        //                {
        //                    foreach (var el in CompaniesDeleg)
        //                    {
        //                        CompaniesDeleg2.Add(el);
        //                    }
        //                }

        //            }
        //            var userCompanies = companyServices.GetCompaniesByUser(user.Id).Distinct().ToList();
        //            foreach (var com in userCompanies)
        //            {
        //                CompaniesDeleg2.Add(com);
        //            }
        //            Companies = CompaniesDeleg2.GroupBy(x => x.Name).Select(t => t.First()).ToList();
        //        }
        //        else
        //        {
        //            Companies = companyServices.GetCompaniesByUser(user.Id).Distinct().ToList();

        //        }
        //    }
        //    return StatusCode(200, Companies);
        //}

  //    [ProducesResponseType(200)]
      //  [ClaimRequirement("Auth", "Authenticated")]
        //[HttpGet, Route("GetCompanyById")]
        //public Task<ActionResult<IEnumerable<CompanyItem>>> GetCompanyById(int id)
        //{
        //    var Company = companyServices.GetCompanyByID(id);
        //    var pays = _paysServices.GetAllPays();
           
        //    var devises = deviseServices.GetAllDevise();
        //    var dev = devises.Single(t => t.Id == Company.DeviseIDConsumed);
        //    var p = pays.Single(pay => pay.Id == Company.PaysIdConsumed);
        //    var CompanyItem = Company.AsDto(p.Code, p.Intitule, p.DeviseCode, dev.Code, dev.Intitule, dev.Decimal, dev.ExchangeRate);
        //    //var congeItemDto = demandeConge.AsDto(empl.NumeroPersonne, empl.Nom);
        //    //  return Ok(congeItemDto);

        //    return Ok(CompanyItem);
        //}

        [ProducesResponseType(200)]
     //   [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetCompanyByName")]
        public IActionResult GetCompanyByName(string name)
        {
            var Company = companyServices.GetCompanyByName(name);
            return StatusCode(200, Company);
        }

     [ProducesResponseType(200)]
    //    [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetCompanyById")]
        public IActionResult GetCompanyById(int id)
        {
            var Company = companyServices.GetCompanyByID(id);
            return StatusCode(200, Company);
        }




        

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_Companies)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Company Company)
        {
            var Rc = new ApplicationUserRoleCompanies();
            try
            {
                var role = UserRolesServices.GetRoleByName("SysAdmin");
                var Admin = UserServices.GetUserByUserName("admin");
                if (Company == null) throw new Exception("Company vide !!");
                if (companyServices.CheckUnicityCompanyByName(Company.Name))
                {
                    var Societe = companyServices.Create(Company);
                    if (Societe == null)
                    {
                        return StatusCode(400, "FailCreateCompany");
                    }
                    else
                    {

                        if (UserRoleCompaniesServices.CheckUnicityUserRoleCompanies(Company.Id, Admin.Id, role.Id))
                        {
                            Rc.companyId = Company.Id;
                            Rc.RoleId = role.Id;
                            Rc.UserId = Admin.Id;
                            UserRoleCompaniesServices.create(Rc);
                        }
                    }
                    return StatusCode(200, Societe);
                }
                else
                {
                    return StatusCode(400, "CompanyExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpPost, Route("POSTCompany")]
        public async Task<IActionResult> POSTCompanyAsync([FromBody] Company Company)
        {
            try
            {
                if (Company == null)
                {
                    return StatusCode(400, "FailEmpty");
                }

                if (companyServices.CheckUnicityCompanyByName(Company.Name))
                {
                    
                    var unt = companyServices.Create(Company);
                    await _publishEndpoint.Publish(new CompanyConsumed(Company.Id, Company.UserCreat, Company.UserModif, Company.DateCreat, Company.DateModif, Company.Name, Company.Description, Company.Adress, Company.Telephone, Company.LegalStatus, Company.FiscalNumber, Company.TradeRegister, Company.Numero, Company.CodePostal, Company.Ville, Company.ComplementAdresse));
                    if (unt == null)
                    {
                        return StatusCode(400, "FailCreateCompany");
                    }
                    return StatusCode(200, unt);
                }
                else
                {
                    return StatusCode(400, "CompanyExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Companies)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Company Company)
        {
            var Societe = new Company();
            try
            {
                if (companyServices.CheckUnicityCompanyByNameID(Company.Name, id))
                {
                    var companyModified = companyServices.GetCompanyByID(id);
                    companyModified.Name = Company.Name;
                    companyModified.Description = Company.Description;
                    companyModified.Adress = Company.Adress;
                    companyModified.Telephone = Company.Telephone;
                    companyModified.LegalStatus = Company.LegalStatus;
                    companyModified.TradeRegister = Company.TradeRegister;
                    companyModified.FiscalNumber = Company.FiscalNumber;

                    companyModified.CodePostal = Company.CodePostal;
                    companyModified.ComplementAdresse = Company.ComplementAdresse;
                    var p = _paysServices.GetPaysByID(Company.PaysIdConsumed);
                    companyModified.DeviseIDConsumed = Company.DeviseIDConsumed;
                    companyModified.PaysIdConsumed = Company.PaysIdConsumed;
                    companyModified.Ville = Company.Ville;
                    companyModified.Numero = Company.Numero;
                    companyServices.Edit(companyModified);
                    return StatusCode(200, companyModified);
                }
                else
                {
                    return StatusCode(400, "CompanyExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_Companies)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Societe = companyServices.Delete(id);
                if (Societe == null)
                {
                    return StatusCode(400, "FailDeleteCompany");
                }
                return StatusCode(200, Societe);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        private readonly IUserServices userServices;
        private readonly IProfileServices _profileServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeServices employeeServices;

        public AccountController(UserManager<ApplicationUser> userManager,
            IUserServices user, IHttpContextAccessor httpContextAccessor, IProfileServices profileServices, IConfiguration configuration, IEmployeeServices emp)
        {
            UserManager = userManager;
            userServices = user;
            _profileServices = profileServices;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            employeeServices = emp;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Users)]
        [HttpPost, Route("POST")]
        public async Task<IActionResult> POST([FromBody] ApplicationUser user)
        {
            var emp = new EmployeeViewModel();
            try
            {
                if ( user.IsActif)
                {
                    return StatusCode(400, "nombreUsers");
                }
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int companyId = int.Parse(session[2].Value);
                var userToAdd = new ApplicationUser
                {
                    UserName = user.UserName,
                    Language = user.Language,
                    IsActif = true,
                    CompanyID = companyId,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(userToAdd.Email);
                if (!match.Success) return StatusCode(400, "formatEmailNonValide");
                var message = userServices.CheckMatriculeEmployee(userToAdd);
                if (message != "") return StatusCode(400, message);
                message = employeeServices.CheckEmployeeExitMail(userToAdd, 0);
                if (message != "") return StatusCode(400, message);
                var PasswordDeHashed = emp.RandomPassword().ToString() + "123*";
                var result = UserManager.CreateAsync(userToAdd, PasswordDeHashed);
                if (!result.Result.Succeeded)
                {
                    return StatusCode(400, result.Result.Errors.First().Code.ToString());
                }
                if (result.Result.Succeeded)
                {
                    var User = _configuration["EmailSettings:UsernameEmail"];
                    var Password = _configuration["EmailSettings:UsernamePassword"];
                    var From = _configuration["EmailSettings:UsernameEmail"];
                    var SmtpClient = _configuration["EmailSettings:host"];
                    int Port = int.Parse(_configuration["EmailSettings:PrimaryPort"]);
                    var ssl = bool.Parse(_configuration["EmailSettings:enableSsl"]);
                    var mail = new MailMessage()
                    {
                        From = new MailAddress(From, "Inn4RH")
                    };

                    string body;
                    if (userToAdd.Language == "fr")
                    {
                        mail.Subject = "Nouveau compte";
                        body = "<p>Bonjour " + userToAdd.Email + " ,</p><b>Votre compte est maintemenat créé.</p><p>Vous trouvez au-dessous votre identifiant et mot de passe: </p><p>Identifiant: " + userToAdd.UserName + "</p><p>Mot de passe: " + PasswordDeHashed + "</p><br><p>Cordialement.</p>";
                    }
                    else
                    {
                        mail.Subject = "Reset Password";
                        body = "<p>Hello " + userToAdd.Email + " ,</p><b>Your new account was created. </p><p>Please check your credential below: </p><p>Identifiant: " + userToAdd.UserName + "</p><p>Mot de passe: " + PasswordDeHashed + "</p><br><p>Best Regards.</p>";
                    }
                    mail.Body = string.Format(body);
                    mail.IsBodyHtml = true;
                    mail.To.Add(userToAdd.Email);
                    using (var smtp = new SmtpClient(SmtpClient, Port))
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(User, Password);
                        smtp.EnableSsl = ssl;
                        await smtp.SendMailAsync(mail);
                    }
                    return StatusCode(200, userToAdd);
                }
                return StatusCode(200, userToAdd);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Users)]
        [HttpGet, Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var listUsers = userServices.GetAllUsers();
            var userName = userServices.GetUserName();
            var filtered = listUsers.Where(u => ((u.UserName != "admin") && (u.UserName != userName))).ToList();
            return StatusCode(200, filtered);
        }

        [ProducesResponseType(200)]
        // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Users)]
        [HttpGet, Route("GetUserById")]
        public IActionResult GetUserById(string id)
        {
            var user = userServices.GetUserByID(id);
            return StatusCode(200, user);
        }

        [ProducesResponseType(200)]
        //   [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetUserByName")]
        public IActionResult GetUserByName(string username)
        {
            var user = userServices.GetUserByUserName(username);
            return StatusCode(200, user);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //     [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Users)]
        [HttpPut, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string id, int idEmpl, [FromBody] ApplicationUser user, string currentLanguage, string appName, string origin)
        {
            try
            {
                var userModified = await UserManager.FindByIdAsync(user.UserName);
                userModified.Language = user.Language;
                userModified.UserName = user.UserName;
                userModified.IsActif = user.IsActif;
                userModified.Email = user.Email;
                userModified.Password = null;
                var res = await UserManager.UpdateAsync(userModified);
                userServices.Edit(userModified);
                var message = userServices.CheckMatriculeEmployee(userModified);
                if (message != "") return StatusCode(400, message);
                if (idEmpl == 0)
                {
                    StringValues requestOriginStrings;
                    string url = "";
                    var found = Request.Headers.TryGetValue("Origin", out requestOriginStrings);
                    var local = requestOriginStrings.ToString().Contains("localhost");
                    if (found && requestOriginStrings.ToString().Contains("localhost"))
                    {
                        url = requestOriginStrings.FirstOrDefault() + "/sendpassword/";
                    }
                    else if ((local == false))
                    {
                        if (appName == "navbar")
                        {
                            url = origin + "/sendpassword/";
                        }
                        else
                        {
                            url = origin + "/" + appName + "/sendpassword/";
                        }
                    }
                    else
                    {
                        return BadRequest("unauthorized");
                    }
                    if (user == null)
                    {
                        return BadRequest("NoUserFound");
                    }
                    string User2 = _configuration["EmailSettings:UsernameEmail"];
                    string Password = _configuration["EmailSettings:UsernamePassword"];
                    string From = _configuration["EmailSettings:UsernameEmail"];
                    string SmtpClient = _configuration["EmailSettings:host"];
                    int Port = int.Parse(_configuration["EmailSettings:PrimaryPort"]);
                    var ssl = bool.Parse(_configuration["EmailSettings:enableSsl"]);
                    try
                    {
                        var mail = new MailMessage()
                        {
                            From = new MailAddress(From, "Inn4RH")
                        };
                        var Token = Convert_StringvalueToHexvalue(user.Email + "*" + DateTime.Now.AddMinutes(30).ToString(), System.Text.Encoding.Unicode);
                        string body;
                        string link2;
                        if (currentLanguage.Equals("fr"))
                        {
                            mail.Subject = "Réinitialisation de mot de passe Inn 4 HR";
                            link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                            body = "<b>Bonjour " + user.UserName + ",<br><b>Votre administrateur a demandé la réinitialisation de votre mot de passe sur Inn 4 HR." +
                                "<br>Poursuivez le processus en cliquant sur le lien au-dessous: <br>" + link2 + "<br>" +
                                "Attention, pour des raisons de sécurité, ce lien n'est valable que pendant 30 minutes. Passé ce délai, cliquez à nouveau sur le bouton 'mot de passe oublié' à l'entrée du site.<br>" +
                                "Cordialement,<br>" +
                                "L'équipe Inn4 HR";
                        }
                        else
                        {
                            mail.Subject = "Inn 4 HR Password Reset";
                            link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                            body = "<b>Hello " + user.UserName + " ,<br><b>Your administrator has requested to reset your password on Inn 4 HR." +
                                "<br>Change your password by clicking on the following link: <br>" + link2 + "<br>" +
                                "Attention, for safety reasons, this link is only valid for 30 minutes. After that, click again on the 'Forgot Password' button at the site entrance.<br>" +
                                "Regards,<br>" +
                                "The Inn4 HR team";
                        }
                        mail.Body = string.Format(body);
                        mail.IsBodyHtml = true;
                        mail.To.Add(user.Email);
                        using (var smtp = new SmtpClient(SmtpClient, Port))
                        {
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(User2, Password);
                            smtp.EnableSsl = ssl;
                            await smtp.SendMailAsync(mail);
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(400, ex);
                    }
                    return Ok();
                }
                else
                {
                    var Employee = new Employee();
                    StringValues requestOriginStrings;
                    string url = "";
                    var found = Request.Headers.TryGetValue("Origin", out requestOriginStrings);
                    var test = requestOriginStrings.ToString().Contains("localhost");
                    if (found && requestOriginStrings.ToString().Contains("localhost"))
                    {
                        url = requestOriginStrings.FirstOrDefault() + "/sendpassword/";
                    }
                    else if ((test == false))
                    {
                        if (appName == "navbar")
                        {
                            url = requestOriginStrings.FirstOrDefault() + "/sendpassword/";
                        }
                        else
                        {
                            url = requestOriginStrings.FirstOrDefault() + "/" + appName + "/sendpassword/";
                        }
                    }
                    else
                    {
                        return BadRequest("unauthorized");
                    }
                    if (user == null)
                    {
                        return BadRequest("NoUserFound");
                    }
                    Employee = employeeServices.GetEmployeeByUserName(user.UserName);

                    var User2 = _configuration["EmailSettings:UsernameEmail"];
                    var Password = _configuration["EmailSettings:UsernamePassword"];
                    var From = _configuration["EmailSettings:UsernameEmail"];
                    var SmtpClient = _configuration["EmailSettings:host"];
                    var Port = int.Parse(_configuration["EmailSettings:PrimaryPort"]);
                    var ssl = bool.Parse(_configuration["EmailSettings:enableSsl"]);
                    try
                    {
                        var mail = new MailMessage()
                        {
                            From = new MailAddress(From, "Inn4RH")
                        };
                        var Token = Convert_StringvalueToHexvalue(user.Email + "*" + DateTime.Now.AddDays(30).ToString(), Encoding.Unicode);
                        string body;
                        string link2;
                        if (currentLanguage.Equals("fr"))
                        {
                            mail.Subject = "Réinitialisation de mot de passe Inn 4 HR";
                            link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                            body = "<b>Bonjour " + Employee.Nom + " " + Employee.Prenom + ",<br><b>Votre administrateur a demandé la réinitialisation de votre mot de passe sur Inn 4 HR." +
                                "<br>Poursuivez le processus en cliquant sur le lien au-dessous: <br><p>" + link2 + "</p><br>" +
                                "Attention, pour des raisons de sécurité, ce lien n'est valable que pendant 30 minutes. Passé ce délai, cliquez à nouveau sur le bouton 'mot de passe oublié' à l'entrée du site.<br>" +
                                "Cordialement,<br>" +
                                "L'équipe Inn4 HR";
                        }
                        else
                        {
                            mail.Subject = "Inn 4 HR Password Reset";
                            link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                            body = "<b>Hello " + Employee.Nom + " " + Employee.Prenom + " ,<br><b>Your administrator has requested to reset your password on Inn 4 HR." +
                                "<br>Change your password by clicking on the following link: </p><br><p>" + link2 + "</p><br>" +
                                "Attention, for safety reasons, this link is only valid for 30 minutes. After that, click again on the 'Forgot Password' button at the site entrance.<br>" +
                                "Regards,<br>" +
                                "The Inn4 HR team";
                        }

                        mail.Body = string.Format(body);
                        mail.IsBodyHtml = true;
                        mail.To.Add(user.Email);
                        using (var smtp = new SmtpClient(SmtpClient, Port))
                        {
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(User2, Password);
                            smtp.EnableSsl = ssl;
                            await smtp.SendMailAsync(mail);
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(400, ex);
                    }
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Users)]
        [HttpPut, Route("PUTAsync")]
        public async Task<IActionResult> PUTAsync(string id, int idEmpl, [FromBody] ApplicationUser user)
        {
            try
            {
                if (  user.IsActif)
                {
                    return StatusCode(400, "nombreUsers");
                }
                var userModified = await UserManager.FindByIdAsync(user.UserName);
                userModified.Language = user.Language;
                userModified.UserName = user.UserName;
                userModified.IsActif = user.IsActif;
                userModified.Email = user.Email;
                userModified.Password = null;
                var message = userServices.CheckMatriculeEmployee(userModified);
                if (message != "") return StatusCode(400, message);
                if (idEmpl == 0)
                {
                    var res = await UserManager.UpdateAsync(userModified);
                    userServices.Edit(userModified);
                    return StatusCode(200, userModified);
                }
                else
                {
                    var empl = new Employee();
                    empl = employeeServices.GetEmployeeByID(idEmpl);
                    empl.Mail = user.Email;
                    empl.Langue = user.Language;
                    empl.User = user.UserName;
                    message = employeeServices.CheckEmployeeExitMail(userModified, empl.Id);
                    if (message != "") return StatusCode(400, message);
                    employeeServices.Edit(empl);
                    var res = await UserManager.UpdateAsync(userModified);
                    userServices.Edit(userModified);
                    return StatusCode(200, userModified);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        private static string Convert_StringvalueToHexvalue(string UserId, Encoding encoding)
        {
            var stringBytes = encoding.GetBytes(UserId);
            var sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        private Boolean verifyLicence()
        {
            int nb = 0;
            nb = (int)long.Parse(_configuration["Licence:licence"]);
            var nbUsersActif = userServices.GetNBActifUsers();
            return (nb > nbUsersActif || nb <= 0);
        }
    }
}
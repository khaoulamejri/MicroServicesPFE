using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public int currentCompanyId = 0;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmployeeServices _employeeServices;
        private readonly IUserServices _userServices;
        private readonly IJwtKeysServices _jwtKeysServices;
        private readonly ICompanyServices _companyServices;
        private readonly IUserRolesServices _userRolesServices;

        public AuthController(IPasswordHasher<ApplicationUser> passwordHasher, ICompanyServices companyServices,
             UserManager<ApplicationUser> UserManager, IConfiguration configuration, IUserServices userServices, IUserRolesServices userRolesServices,
              SignInManager<ApplicationUser> signInManager, IEmployeeServices employeeServices, IJwtKeysServices jwtKeysServices)
        {
            _configuration = configuration;
            _UserManager = UserManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _employeeServices = employeeServices;
            _userServices = userServices;
            _jwtKeysServices = jwtKeysServices;
            _companyServices = companyServices;
            _userRolesServices = userRolesServices;
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

        private static string Convert_HexvalueToStringvalue(string hexvalue, Encoding encoding)
        {
            var CharsLength = hexvalue.Length;
            var bytesarray = new byte[CharsLength / 2];
            for (int i = 0; i < CharsLength; i += 2)
            {
                bytesarray[i / 2] = Convert.ToByte(hexvalue.Substring(i, 2), 16);
            }
            return encoding.GetString(bytesarray);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("Reset")]
        public async Task<IActionResult> Reset(string email, string currentLanguage, string appName, string origin)
        {
            try
            {
                var Employee = new Employee();
                StringValues requestOriginStrings;
                var url = "";
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
                var user = await _UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest("NoUserFound");
                }
                Employee = _employeeServices.GetEmployeeByUserName(user.UserName, user.CompanyID);
                var User = _configuration["EmailSettings:UsernameEmail"];
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
                    var Token = Convert_StringvalueToHexvalue(email + "*" + DateTime.Now.AddDays(30).ToString(), Encoding.Unicode);
                    string body;
                    string link2;
                    if (currentLanguage.Equals("fr"))
                    {
                        mail.Subject = "Réinitialisation de mot de passe Inn 4 HR";
                        link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                        body = "<b>Bonjour " + Employee.Nom + " " + Employee.Prenom + ",<br><b>Vous avez demandé la réinitialisation de votre mot de passe sur Inn 4 HR." +
                            "<br>Poursuivez le processus en cliquant sur le lien au-dessous: <br>" + link2 + "<br>" +
                            "Attention, pour des raisons de sécurité, ce lien n'est valable que pendant 30 minutes. Passé ce délai, cliquez à nouveau sur le bouton 'mot de passe oublié' à l'entrée du site.<br>" +
                            "Cordialement,<br>" +
                            "L'équipe Inn4 HR";
                    }
                    else
                    {
                        mail.Subject = "Inn 4 HR Password Reset";
                        link2 = "<a href=" + url + currentLanguage + "/" + Token + ">Réinitialisation du mot de passe</a>";
                        body = "<b>Hello " + Employee.Nom + " " + Employee.Prenom + " ,<br><b>You have requested to reset your password on Inn 4 HR." +
                            "<br>Change your password by clicking on the following link: <br>" + link2 + "<br>" +
                            "Attention, for safety reasons, this link is only valid for 30 minutes. After that, click again on the 'Forgot Password' button at the site entrance.<br>" +
                            "Regards,<br>" +
                            "The Inn4 HR team";
                    }

                    mail.Body = string.Format(body);
                    mail.IsBodyHtml = true;
                    mail.To.Add(email);
                    using (var smtp = new SmtpClient(SmtpClient, Port))
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(User, Password);
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
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string password)
        {

            try
            {
                var Decryptedtoken = Convert_HexvalueToStringvalue(token, Encoding.Unicode);
                var id = Decryptedtoken.Split('*')[0];
                var date = Decryptedtoken.Split('*')[1];
                var DecryptedDateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                var result = DateTime.Compare(DateTime.Now, DecryptedDateTime);
                var user = _userServices.GetUserByEmail(id);
                if (user == null)
                {
                    return BadRequest("User NOT_FOUND");
                }
                if (result > 0)
                {
                    return BadRequest("EXPIRED_DATE");
                }
                user.PasswordHash = _UserManager.PasswordHasher.HashPassword(user, password);
                _userServices.Edit(user);
                return Ok("OK");
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }
        //[HttpPost]
        //[Route("Register")]

        //public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        //{
        //    var applicationUser = new ApplicationUser()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FullName = model.FullName
        //    };

        //    try
        //    {
        //        var result = await _UserManager.CreateAsync(applicationUser, model.Password);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost("registre")]
        public async Task<IActionResult> registre([FromBody] RegisterAdminViewModel userregister)
        {
            var userexist = await _UserManager.FindByNameAsync(userregister.UserName);
            if (userexist != null)
                return StatusCode(500, "INVALIDE");
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userregister.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = userregister.Email,
                Language = userregister .Language


                    

            };
            var result = await _UserManager.CreateAsync(user, userregister.Password);
            if (!result.Succeeded)
            {
                return StatusCode(500, "INVALIDE");
            }


            return Ok("SUCCESSFUL_REQUEST");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [HttpPut("EditPassword")]
        public async Task<IActionResult> EditPassword([FromBody] RegisterViewModel userregister)
        {
            var user = _userServices.GetUserByUserName(userregister.UserName);
            if (!string.IsNullOrEmpty(userregister.ConfirmPassword) && !string.IsNullOrEmpty(userregister.Password))
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userregister.Password) == PasswordVerificationResult.Success)
                {
                    user.Password = userregister.ConfirmPassword;
                    user.PasswordHash = _UserManager.PasswordHasher.HashPassword(user, userregister.ConfirmPassword);
                }
                else
                {
                    return StatusCode(409, "INVALIDE_PASSWORD");
                }
            }
            _userServices.Edit(user);
            return Ok("SUCCESSFUL_REQUEST");
        }


        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult unAuth()
        {
            return StatusCode(401);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost, Route("token")]
        public async Task<IActionResult> PostAsync([FromBody] LoginViewModel loginModel)
        {
            var verifyPass = false;
            var users = _userServices.GetUserByUserName(loginModel.UserName);
            //if (!users.IsActif)
            //{
            //    return StatusCode(400, "UserInactif");
            //}
            //if (!verifyLicence())
            //{
            //    return StatusCode(400, "LicenceExpired");
            //}
            var user = _signInManager.UserManager.FindByIdAsync(users.Id);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    verifyPass = true;
                }
            }
            var ap = new ApplicationUser();
            ap = _userServices.GetUserByUserName(loginModel.UserName);
            var company = new Company();
            var empl = _employeeServices.GetEmployeeByUserName(ap.UserName);
            var employeeIdentifier = empl != null ? empl.Id.ToString() : "";
            var companies = _companyServices.GetCompaniesByUser(ap.Id);
            List<string> Role = null;
            foreach (var cmp in companies)
            {
                Role = _userRolesServices.getListRolesNames(ap.Id, cmp.Id);
                if (Role.Count > 0)
                {
                    company = cmp;
                    break;
                }
            }
            if (Role.Count == 0)
            {
                return BadRequest("NoRoleAffected");
            }
            if (verifyPass)
            {
                var tokenDescriptor = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Rsa, company.Name),
                    new Claim(ClaimTypes.PrimarySid, company.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, ap.Language),
                    new Claim(ClaimTypes.IsPersistent, loginModel.RememberMe.ToString()),
                    new Claim(ClaimTypes.Expiration, loginModel.RememberMe ? DateTime.Now.AddMinutes(60).ToString() : DateTime.Now.AddMinutes(60).ToString())
                 //   new Claim(ClaimTypes.Actor, employeeIdentifier)
               
                };
                foreach (string r in Role)
                {
                    tokenDescriptor.Add( new Claim(ClaimTypes.Role, r));
                    // tokenDescriptor.Subject.AddClaims(new[] { new Claim(ClaimTypes.Role, r) });
                }
                var token =
                    new JwtSecurityToken(issuer: _configuration["Token:Issuer"],
                                        audience: _configuration["Token:Audience"],
                                        claims: tokenDescriptor,
                                        expires: loginModel.RememberMe ? DateTime.Now.AddHours(1) : DateTime.Now.AddHours(1),
                                        notBefore: DateTime.Now,
                                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                                        SecurityAlgorithms.HmacSha512));

                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = GenerateRefreshToken();
                var tokens = new Tokens()
                {
                    accessToken = tokenstring,
                    refreshToken = refreshToken
                };
                try
                {
                    var key = new JwtKeys()
                    {
                        jwtKey = tokenstring,
                        refreshToken = refreshToken,
                        userId = ap.UserName
                    };
                    _jwtKeysServices.Create(key);
                }
                catch (Exception ed)
                {
                    var cst = ed;
                }
                return StatusCode(200, tokens);
            }
            return StatusCode(200, "");
        }

        private Boolean verifyLicence()
        {
            if (_configuration["Licence:exp"].Length == 0)
                return true;
            DateTime dateExpiration;
            var currentDate = DateTime.Now;
            try
            {
                dateExpiration = Convert.ToDateTime(_configuration["Licence:exp"]);
            }
            catch
            {
                dateExpiration = currentDate;
                return (dateExpiration > currentDate);
            }
            return (dateExpiration > currentDate);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost, Route("PostLogOut")]
        public JsonResult PostLogOut([FromBody] Tokens tokens)
        {
            if (string.IsNullOrEmpty(tokens.accessToken)) return new JsonResult("Error: empty token");

            var username = tokens.userName;

            JwtKeys JwtKey = _jwtKeysServices.GetByUserId(username);
            if (JwtKey == null) return new JsonResult("Error: key not found");

            _jwtKeysServices.Delete(JwtKey);
            return new JsonResult("Ok");

        }

        [HttpGet, Route("Postssrstoken")]
        public IActionResult Postssrstoken(string token)
        {
            try
            {
                var stc = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var sa = @"""" + "/Date(" + stc.Payload.Exp.Value.ToString() + "000-0500)/" + @"""";
                var expires = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(sa);
                var checkUser = _jwtKeysServices.GetJwtKeysByKey(token).Count() > 0;
                if (!string.IsNullOrWhiteSpace(token) && checkUser && expires >= DateTime.Now) return Content("true");
                return Content("false");
            }
            catch (Exception ex)
            {
                return Content("false");
            }
        }

        [ProducesResponseType(200)]
        [HttpPost, Route("PostChangeCMP")]
        public IActionResult PostChangeCMP([FromBody] LoginViewModel loginModel)
        {
            var cmp = _companyServices.GetCompanyByName(loginModel.Company);
            var user = _userServices.GetUserByUserName(loginModel.UserName);
            user.CompanyID = cmp.Id;
            _userServices.Edit(user);
            return Ok(UpdateToken(loginModel.UserName, loginModel.Role, loginModel.Company));
        }

        [ProducesResponseType(200)]
        [HttpPut]
        public IActionResult UpdateToken(string username, List<string> ListRoles, string Listcompany, int expireMinutes = 20)
        {
            string societe = null;
            var symmetricKey = _configuration["Token:Key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var ap = _userServices.GetUserByUserName(username);
            var company = _companyServices.GetCompanyByID(ap.CompanyID);

            if (!Listcompany.Equals("undefined"))
            {
                societe = Listcompany;
            }
            else
            {
                societe = company.Name;
            }
            List<string> Roles = null;
            var compan = _companyServices.GetCompanyByName(societe);

            Roles = _userRolesServices.getListRolesNames(ap.Id, compan.Id);

            Roles.ToList();
            if (Roles.Count() == 0)
            {
                return StatusCode(400, "RoleUndefined");
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Rsa, company.Name),
                    new Claim(ClaimTypes.PrimarySid, company.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, ap.Language),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(60).ToString())
                }),
            };
            foreach (string role in Roles)
            {
                tokenDescriptor.Subject.AddClaims(new[] { new Claim(ClaimTypes.Role, role) });
            }

            var tokenstring = GenerateToken(username, societe);
            return Ok(tokenstring);
        }

        //[HttpGet, Route("GetAllStaticPrivilege")]
        //public IActionResult GetAllStaticPrivilege()
        //{
        //    var privileges = ApiPrivileges.GetFieldValues();
        //    return StatusCode(200, privileges);
        //}
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost, Route("Refresh")]
        public IActionResult Refresh([FromBody] Tokens tokens)
        {
            //var principal = GetPrincipalFromExpiredToken(tokens.accessToken);
            //var username = principal.Identity.Name;
            var username = tokens.userName;
            JwtKeys JwtKey = _jwtKeysServices.GetByUserId(username);
            if (JwtKey == null)
                return NotFound();
            var savedRefreshToken = JwtKey.refreshToken; //retrieve the refresh token from a data store
            if (savedRefreshToken != tokens.refreshToken)
                return StatusCode(200, "");

            var newJwtToken = GenerateToken(username, tokens.companyName);
            var newRefreshToken = GenerateRefreshToken();
            JwtKey.refreshToken = newRefreshToken;
            JwtKey.jwtKey = newJwtToken;
            _jwtKeysServices.Edit(JwtKey);

            var newTokens = new Tokens()
            {
                accessToken = newJwtToken,
                refreshToken = newRefreshToken
            };
            return StatusCode(200, newTokens);
        }



        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Token:Issuer"],

                ValidateAudience = true,
                ValidAudience = _configuration["Token:Audience"],

                ValidateIssuerSigningKey = true,/*Configuration["JwtSecurityToken:Key"])*/
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }


        private string GenerateToken(string userName, string companyName)
        {
            var user = _userServices.GetUserByUserName(userName);
            var empl = _employeeServices.GetEmployeeByUserName(userName);
            var employeeIdentifier = empl != null ? empl.Id.ToString() : "";
            var company = _companyServices.GetCompanyByID(user.CompanyID);

            if (companyName.Equals("undefined"))
            {
                companyName = company.Name;
            }

            List<string> Roles = null;
            var compan = _companyServices.GetCompanyByName(companyName);

            Roles = _userRolesServices.getListRolesNames(user.Id, compan.Id);

            Roles.ToList();


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                   {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Rsa, compan.Name),
                    new Claim(ClaimTypes.PrimarySid, compan.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Language),
                    new Claim(ClaimTypes.IsPersistent, "true"),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(60).ToString() ),
                    new Claim(ClaimTypes.Actor, employeeIdentifier)
                }),
            };
            foreach (string r in Roles)
            {
                tokenDescriptor.Subject.AddClaims(new[] { new Claim(ClaimTypes.Role, r) });
            }
            var jwt = new JwtSecurityToken(issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: tokenDescriptor.Subject.Claims, //the user's claims, for example new Claim[] { new Claim(ClaimTypes.Name, "The username"), //... 
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                SecurityAlgorithms.HmacSha512));

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string
        }
    }
}

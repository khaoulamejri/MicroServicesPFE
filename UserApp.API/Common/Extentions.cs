using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using static UserApp.API.Common.DTO;

namespace UserApp.API.Common
{
    public static class Extentions
    {
       
        public static CompanyItem AsDto(this Company item, string Code, string Intitule, string DeviseCode, string CodeDevise, string IntituleDevise, int Decimal, float ExchangeRate)
   
        {
            return new CompanyItem(item.Id, item.UserCreat, item.UserModif, item.DateCreat, item.DateModif, item.Name, item.Description, item.Adress, item.Telephone, item.LegalStatus, item.FiscalNumber, item.TradeRegister, item.Numero, item.CodePostal, item.Ville, item.ComplementAdresse, item.PaysIdConsumed, Code, Intitule, DeviseCode, item.DeviseIDConsumed, CodeDevise, IntituleDevise, Decimal, ExchangeRate);
        }
    }

}

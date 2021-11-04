using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApp.API.Common
{
    public class DTO
    {
        public record DemandeCongeItemDto(Guid IDEmployeeConsumed, string NumeroPersonne, string Nom, Guid IDCompanyConsumed, string Name, string Adress, String Numero, float NbrConge, DateTime DateDemande);
        public record CompanyItem(int Id, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, string Name, string Description, string Adress, string Telephone, string LegalStatus, string FiscalNumber, string TradeRegister, string Numero, string CodePostal, string Ville, string ComplementAdresse, int PaysIdConsumed, string Code, string Intitule, string DeviseCode, int DeviseIDConsumed, string CodeDevise, string IntituleDevise, int Decimal, float ExchangeRate);
    }
}

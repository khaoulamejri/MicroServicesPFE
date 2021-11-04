using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conge.Data.Common.DTOS;

namespace Conge.Services.Iservices
{
    public interface ITitreCongeServices
    {
        List<TitreCongeDto> GetAllTitreConge();
        TitreConge GetTitreCongeByID(int id);
        List<TitreCongeDto> GetTitreCongeByEmployeeID(int id);
        TitreConge Create(TitreConge TitreConge);
        TitreConge Edit(TitreConge TitreConge);
        double GetAutomaticNumber(DateTime startDate, DateTime endDate, int EmployeeId, Boolean matinYNdeb, Boolean MatinYNfin);
        TitreConge GenererTitre(DemandeConge DemandeConge);

    }
}

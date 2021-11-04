using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
  public  interface IAffectationEmployeeServices
    {  
        AffectationEmployee Create(AffectationEmployee AffectationEmployee);
    List<Position> GetAllVacantPositionByIntervalleDate(DateTime Startdate, DateTime EndDate);
    List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id);
    List<AffectationEmployee> GetAffectationEmployeeByEmployeeIdByCMPSelected(int id);
    List<SelectListItem> SelectListItemEmployee();
   List<Position> SelectListItemPositions(DateTime dateDeb, DateTime dateFin);
    AffectationEmployee GetAffectationEmployeeByID(int id);
    List<AffectationEmployee> GetAffectationEmployeeByEmployeeIdcmp(int id);
    AffectationEmployee Edit(AffectationEmployee AffectationEmployee);
    string Delete(int AffectationEmployeeId);
    AffectationEmployee GetAffectationActif(int EmployeeId);
    AffectationEmployee GetAffectationActifByCMPSelected(int EmployeeId);
    AffectationEmployee GetAffectationActifWithoutCMP(int EmployeeId);
    AffectationEmployee GetAffectationActifcmp(int EmployeeId);
    List<AffectationEmployee> GetListAffectationEmployeeByPositionID(int id);
    bool CheckUnicityPrincipalAffectationEmployee(int id, int employeeID, int companyID);
    bool CheckAffectationExistingTimeInterval(int positionID, DateTime dateDebut, DateTime dateFin, int employeeID);
}
}

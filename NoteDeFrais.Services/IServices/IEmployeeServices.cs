using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IEmployeeServices
    {
       
        List<Employee> GetAllEmployees();
        
        Employee GetEmployeeByID(int id);
        Employee GetEmployeeByUserNameCompany();


        Employee Create(Employee Employee);
        Employee Edit(Employee Employee);
        Employee GetEmployeeByUserName(string user);
        Employee Delete(int EmployeeId);
        List<WFDocument> GetDocumentsToBeValidatedByEmployee(int employeeId, string documentType);
        string checkMiisonOrdersAndLeaveDates(int employeeId, DateTime dateDebutOrdre, DateTime dateFinOrdre);
        List<DemandeConge> getValidatedLeavesByEmployeeId(int employeeId);
        List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(int companyId, string userName, bool SupInclut = true);

    }
}

using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
   public interface IEmployeeServices
    {
        List<Employee> GetAllEmployee();
        List<Employee> GetAllEmployees();
        List<Employee> GetAllEmployeecmp(int companyId);
        Employee GetEmployeeByID(int id);
        Employee GetEmployeByID(int? id);
        Employee GetEmployeeByUserName(string user);
        Employee GetEmployeeByUserName(string user, int cmpId);
        Employee GetEmployeeByUserNameCompany();
        Employee Create(Employee Employee);
        Employee Edit(Employee Employee);
        byte[] GetProfileImage(string userId);
        Employee Delete(int EmployeeId);
        List<Employee> SelectListItemEmployeeHierarchy(bool SupInclut = true, bool AddAll = false);
        List<Employee> SelectListItemSoldeEmployeeHierarchy(bool SupInclut = true, bool AddAll = false);
        List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(bool SupInclut = true);
        List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(int companyId, string userName, bool SupInclut = true);
        List<Employee> ListEmpLByhierarchy(bool SupInclut = true);
        List<Employee> ListEmpLByhierarchyByCMPSelected(bool SupInclut = true);
        List<Employee> ListEmpLByhierarchyWithoutCMP(bool SupInclut = true);
        //List<SelectListItem> SelectListItemEmployeeWithoutuser();
        List<Employee> GetAllEmployeeByCompanyId();
        List<Employee> ListEmpLByhierarchyWithoutCompany(bool SupInclut = true);
      //  List<DestinataireViewModel> GetAllDestinatairesByCompanyId(int companyId);
        //string CheckEmployeeExitMail(ApplicationUser user, int employeeId);
   string CheckUnicityEmployeeParameterCreate(List<Employee> employees, EmployeeViewModel Employee);
        string CheckUnicityEmployeeParameterUpdate(List<Employee> employees, EmployeeViewModel Employee);
        string CheckUnicityEmployeeMailAffectToUser(Employee Employee);
        bool CheckUnicityEmployeeMailGenerateUser(Employee empl);
        List<Employee> GetAllEmployeeRemplacant(bool isSysAdmin, int id);
        List<Employee> GetAllEmployeeRemplacant(bool isSysAdmin, int id, int companyID);
        List<Employee> GetAllEmployeeByCompanyId(int companyID);
        List<Employee> ListSoldeEmpLByhierarchy(bool SupInclut = true);
        bool checkEmployeeHaveWFLeave();
        bool checkEmployeeHaveWFOM();
        bool checkEmployeeHaveWFExpense();
        string CheckUnicityEmployeeParameterCreat(List<Employee> employees, Employee Employee);
        bool CheckUnicityEmploye(string nom);
    }
}

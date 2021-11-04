using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
public    interface IEmployeeServices
    {
        Employee GetEmployeeByID(int id);
        public RemplacantModel GetRemplacantByID(int id);
        public List<Employee> GetAllEmployeeRemplacantt(bool isSysAdmin, int id);
        public List<RemplacantModel> GetAllEmployeeRemplacant(int id);
        List<Employee> GetAllEmployee();
        Employee Create(Employee Employee);
        Employee Edit(Employee Employee);
        public List<Employee> GetAllEmployeeByCompanyId();
        Employee Delete(int EmployeeId);
        AffectationEmployee GetAffectationActif(int EmployeeId);
        List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id);
    }
}

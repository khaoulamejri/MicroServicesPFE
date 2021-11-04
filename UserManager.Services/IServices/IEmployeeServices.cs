using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
 public   interface IEmployeeServices
    {
        Employee Create(Employee Employee);
        Employee Edit(Employee Employee);
        Employee Delete(int EmployeeId);
        Employee GetEmployeeByID(int id);
        string CheckEmployeeExitMail(ApplicationUser user, int employeeId);
        Employee GetEmployeeByUserName(string user);
        Employee GetEmployeeByUserName(string user, int cmpId);
    }
}

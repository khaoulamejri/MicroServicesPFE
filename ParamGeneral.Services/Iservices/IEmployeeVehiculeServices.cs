using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface IEmployeeVehiculeServices
    {
        EmployeeVehicule GetEmployeeVehiculeByID(int id);
        List<EmployeeVehicule> GetAllEmployeeVehicule();
        EmployeeVehicule Create(EmployeeVehicule employeeVehicule);
        EmployeeVehicule Edit(EmployeeVehicule employeeVehicule);
        EmployeeVehicule Delete(int employeeVehiculeId);
        List<EmployeeVehicule> GetAllEmployeeVehiculeByEmployeeId(int id);
        List<EmployeeVehicule> GetAllEmployeeVehiculeByEmployeeIdAndDate(int id, DateTime dateDebutNote, DateTime dateFinNote);
        List<Boolean> GetAllTypeVehiculeByEmployeeId(int id);
        bool checkUnicity(EmployeeVehicule employeeVehicule);

    }
}

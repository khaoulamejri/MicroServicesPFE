using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IEmployeeGroupeServices
    {
        EmployeeGroupe GetEmployeeGroupeByIdIncluded(int id);
        EmployeeGroupe GetEmployeeGroupeById(int id);
        List<EmployeeGroupe> GetAllEmployeeGroupe();
        EmployeeGroupe Create(EmployeeGroupe employeeGroupe);
        EmployeeGroupe Edit(EmployeeGroupe employeeGroupe);
        EmployeeGroupe Delete(int employeeGroupeId);
        List<EmployeeGroupe> GetAllEmployeeGroupeByEmployeeId(int id);
        List<GroupeFrais> GetAllGroupeFraisByEmployee(int id);
        GroupeFrais GetGroupeByEmployeeIdDateNote(int id, DateTime startDate, DateTime endDate);
        List<EmployeeGroupe> GetGroupeByEmployeeIdIncludeGroupeFraisDepense(int employeeId);
        bool checkEmployeeGroup(EmployeeGroupe employeeGroupe);


    }
}

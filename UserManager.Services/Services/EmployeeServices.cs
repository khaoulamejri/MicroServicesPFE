using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        public EmployeeServices(ApplicationDbContext context)
        {
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public Employee Create(Employee Employee)
        {
            try
            {
                utOfWork.EmployeeRepository.Add(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Employee GetEmployeeByUserName(string user)
        {
            return utOfWork.EmployeeRepository.GetMany(d => d.User == user).FirstOrDefault();
        }

        public Employee Delete(int EmployeeId)
        {

            try
            {
                var employee = GetEmployeeByID(EmployeeId);
                if (employee != null)
                {
                    utOfWork.EmployeeRepository.Delete(employee);
                    utOfWork.Commit();
                    return employee;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Employee Edit(Employee Employee)
        {
            try
            {
                utOfWork.EmployeeRepository.Update(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Employee GetEmployeeByID(int id)
        {
            return utOfWork.EmployeeRepository.GetAll().FirstOrDefault(d => d.Id == id);

        }
        public Employee GetEmployeeByUserName(string user, int cmpId)
        {
            return utOfWork.EmployeeRepository.GetMany(d => d.User == user && d.companyID == cmpId).FirstOrDefault();
        }

        public string CheckEmployeeExitMail(ApplicationUser user, int employeeId)
        {
            if ((utOfWork.EmployeeRepository.GetMany(d => d.Mail == user.Email && d.Mail != null).Any()) && user.Email != null && user.Email != "")
            {
                return "EmployeeExitMail";
            }
            else return "";
        }
    }
}

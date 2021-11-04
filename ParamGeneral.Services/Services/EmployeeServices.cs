using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParamGeneral.Data;
using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ParamGeneral.Services.Services
{
    public class EmployeeServices : IEmployeeServices

    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        //private readonly IParamGenerauxServices paramGenerauxServices;
        //private readonly ITypeHierarchyPositionsServices typeHierarchyPositionsServices;
        //private readonly IHierarchyPositionServices hierarchyPositionServices;
        private readonly IAffectationEmployeeServices affectationEmployeeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, IAffectationEmployeeServices aff)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            //paramGenerauxServices = p;
            //typeHierarchyPositionsServices = thp;
            //hierarchyPositionServices = hp;
            affectationEmployeeServices = aff;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public bool checkEmployeeHaveWFExpense()
        {
            throw new NotImplementedException();
        }

        public bool checkEmployeeHaveWFLeave()
        {
            throw new NotImplementedException();
        }

        public bool checkEmployeeHaveWFOM()
        {
            throw new NotImplementedException();
        }

        public string CheckUnicityEmployeeMailAffectToUser(Employee Employee)
        {
            throw new NotImplementedException();
        }

        public bool CheckUnicityEmployeeMailGenerateUser(Employee empl)
        {
            throw new NotImplementedException();
        }

        public Employee Create(Employee Employee)
        {
            try
            {
              //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
             // int currentCompanyId = int.Parse(session[2].Value);
       //   Employee.companyID = currentCompanyId;

                utOfWork.EmployeeRepository.Add(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Employee Delete(int EmployeeId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var Employee = utOfWork.EmployeeRepository.GetMany(w => w.Id == EmployeeId).Include(x => x.AffectationEmployee).SingleOrDefault();
                    if (Employee != null)
                    {
                        foreach (var item in Employee.AffectationEmployee)
                        {
                            affectationEmployeeServices.Delete(item.Id);
                        }
                        utOfWork.EmployeeRepository.Delete(Employee);
                        utOfWork.Commit();
                        scope.Complete();
                        return Employee;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        public Employee Edit(Employee Employee)
        {
            try
            {
                Employee.RegimeTravail = null;
                utOfWork.EmployeeRepository.Update(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Employee> GetAllEmployee()
        {
       return utOfWork.EmployeeRepository.GetAll().ToList();
          
        }

        public List<Employee> GetAllEmployeeByCompanyId()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployeeByCompanyId(int companyID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployeecmp(int companyId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployeeRemplacant(bool isSysAdmin, int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployeeRemplacant(bool isSysAdmin, int id, int companyID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
           // var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            return utOfWork.EmployeeRepository.GetAll().ToList();
        }
        public string CheckUnicityEmployeeParameterCreate(List<Employee> employees, EmployeeViewModel Employee)
        {
            if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && (employees.Any(d => d.Mail == Employee.Mail && d.Mail != null))
                && (employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitMatriculeCINMail";
            }
            else if ((employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))) && (employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)))
            {
                return "EmployeeExitMatriculeCIN";
            }
            else if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && (employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMatriculeMail";
            }
            else if ((employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)) && (employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCINMail";
            }
            else if ((employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCIN";
            }
            else if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)))
            {
                return "EmployeeExitMatricule";
            }
            else if ((employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMail";
            }
            else if (!(employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))) && !(employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && !(employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
                return "";
            else
                return null;
        }
        public string CheckUnicityEmployeeParameterCreat(List<Employee> employees, Employee Employee)
        {
            if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && (employees.Any(d => d.Mail == Employee.Mail && d.Mail != null))
                && (employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitMatriculeCINMail";
            }
            else if ((employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))) && (employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)))
            {
                return "EmployeeExitMatriculeCIN";
            }
            else if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && (employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMatriculeMail";
            }
            else if ((employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)) && (employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCINMail";
            }
            else if ((employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCIN";
            }
            else if ((employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)))
            {
                return "EmployeeExitMatricule";
            }
            else if ((employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMail";
            }
            else if (!(employees.Any(d => d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))) && !(employees.Any(d => d.NumeroPersonne == Employee.NumeroPersonne)) && !(employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
                return "";
            else
                return null;
        }

        public string CheckUnicityEmployeeParameterUpdate(List<Employee> employees, EmployeeViewModel Employee)
        {
            if ((employees.Any(d => d.Id != Employee.Id && d.Mail == Employee.Mail && d.Mail != null)) && (employees.Any(d => d.Id != Employee.Id && d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCINMail";
            }
            else if ((employees.Any(d => d.Id != Employee.Id && d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMail";
            }
            else if ((employees.Any(d => d.Id != Employee.Id && d.CIN == Employee.CIN && !String.IsNullOrEmpty(Employee.CIN))))
            {
                return "EmployeeExitCIN";
            }
            else if ((employees.Any(d => d.Mail == Employee.Mail && d.Mail != null)))
            {
                return "EmployeeExitMail";
            }
            else
                return null;
        }

        public Employee GetEmployeeByID(int id)
        {
            return utOfWork.EmployeeRepository.GetAll().Include(a => a.AffectationEmployee).FirstOrDefault(d => d.Id == id);
        }
        public Employee GetEmployeByID(int? id)
        {
            return utOfWork.EmployeeRepository.GetAll().Include(a => a.AffectationEmployee).FirstOrDefault(d => d.Id == id);
        }

        public Employee GetEmployeeByUserName(string user)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByUserName(string user, int cmpId)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByUserNameCompany()
        {
            throw new NotImplementedException();
        }

        public byte[] GetProfileImage(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListEmpLByhierarchy(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListEmpLByhierarchyByCMPSelected(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListEmpLByhierarchyWithoutCMP(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListEmpLByhierarchyWithoutCompany(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListSoldeEmpLByhierarchy(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(int companyId, string userName, bool SupInclut = true)
        {
            throw new NotImplementedException();
        }

        public List<Employee> SelectListItemEmployeeHierarchy(bool SupInclut = true, bool AddAll = false)
        {
            throw new NotImplementedException();
        }

        public List<Employee> SelectListItemSoldeEmployeeHierarchy(bool SupInclut = true, bool AddAll = false)
        {
            throw new NotImplementedException();
        }

        public bool CheckUnicityEmploye(string nom)
        {
           
                return !utOfWork.EmployeeRepository.GetMany(a => a.Nom == nom).Any();
           
        }
    }
}

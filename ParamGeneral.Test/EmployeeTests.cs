using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParamGeneral.Test
{
    [TestClass]
    public class EmployeeTests
    {
        //[TestMethod]
        //public void EmployeeExitMatriculeCINMail()
        //{
        //    //var setupService = new SetupServices();
        //    //var provider = setupService.SetupProvider();
        //    //var service = provider.GetService<IEmployeeServices>();
        //    //var listEmployee = new List<Employee>();
        //    //var employee1 = new Employee
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};
        //    //var employee2 = new Employee
        //    //{
        //    //    CIN = "006822222",
        //    //    Mail = "nadra.msallem22@gmail.com",
        //    //    NumeroPersonne = "045881"
        //    //};
        //    //var employee3 = new Employee
        //    //{
        //    //    CIN = "0068233",
        //    //    Mail = "nadra.msallem232@gmail.com",
        //    //    NumeroPersonne = "04588331"
        //    //};
        //    //listEmployee.Add(employee1);
        //    //listEmployee.Add(employee2);
        //    //listEmployee.Add(employee3);

        //    //var employeeToCreate = new EmployeeViewModel
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};

        //    //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        //    //var expected = "EmployeeExitMatriculeCINMail";
        //    //Assert.Equal(expected, actual);
        //}

        //[TestMethod]
        //public void EmployeeExitMatriculeCIN()
        //{
        ////    var setupService = new SetupServices();
        ////    var provider = setupService.SetupProvider();
        ////    var service = provider.GetService<IEmployeeServices>();
        ////    var listEmployee = new List<Employee>();
        ////    var employee1 = new Employee
        ////    {
        ////        CIN = "00684691",
        ////        Mail = "nadra.msallem@gmail.com",
        ////        NumeroPersonne = "000001"
        ////    };
        ////    var employee2 = new Employee
        ////    {
        ////        CIN = "006822222",
        ////        Mail = "nadra.msallem22@gmail.com",
        ////        NumeroPersonne = "045881"
        ////    };
        ////    var employee3 = new Employee
        ////    {
        ////        CIN = "0068233",
        ////        Mail = "nadra.msallem232@gmail.com",
        ////        NumeroPersonne = "04588331"
        ////    };
        ////    listEmployee.Add(employee1);
        ////    listEmployee.Add(employee2);
        ////    listEmployee.Add(employee3);

        ////    var employeeToCreate = new EmployeeViewModel
        ////    {
        ////        CIN = "00684691",
        ////        Mail = "nadra.msallem12345@gmail.com",
        ////        NumeroPersonne = "000001"
        ////    };

        ////    var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        ////    var expected = "EmployeeExitMatriculeCIN";
        ////    Assert.Equal(expected, actual);
        //}

        //[TestMethod]
        //public void EmployeeExitMatriculeMail()
        //{
        //    //var setupService = new SetupServices();
        //    //var provider = setupService.SetupProvider();
        //    //var service = provider.GetService<IEmployeeServices>();
        //    //var listEmployee = new List<Employee>();
        //    //listEmployee = service.GetAllEmployee();
        //    ////var employee1 = new Employee
        //    ////{
        //    ////    CIN = "00684691",
        //    ////    Mail = "nadra.msallem@gmail.com",
        //    ////    NumeroPersonne = "000001"
        //    ////};
        //    ////var employee2 = new Employee
        //    ////{
        //    ////    CIN = "006822222",
        //    ////    Mail = "nadra.msallem22@gmail.com",
        //    ////    NumeroPersonne = "045881"
        //    ////};
        //    ////var employee3 = new Employee
        //    ////{
        //    ////    CIN = "0068233",
        //    ////    Mail = "nadra.msallem232@gmail.com",
        //    ////    NumeroPersonne = "04588331"
        //    ////};
        //    ////listEmployee.Add(employee1);
        //    ////listEmployee.Add(employee2);
        //    ////listEmployee.Add(employee3);

        //    //var employeeToCreate = new EmployeeViewModel
        //    //{
        //    //    CIN = "00684691000",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};

        //    //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        //    //var expected = "EmployeeExitMatriculeMail";
        //    //Assert.Equal(expected, actual);
        //}

        //[TestMethod]
        //public void EmployeeExitCIN()
        //{
        //    //var setupService = new SetupServices();
        //    //var provider = setupService.SetupProvider();
        //    //var service = provider.GetService<IEmployeeServices>();
        //    //var listEmployee = new List<Employee>();
        //    //var employee1 = new Employee
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};
        //    //var employee2 = new Employee
        //    //{
        //    //    CIN = "006822222",
        //    //    Mail = "nadra.msallem22@gmail.com",
        //    //    NumeroPersonne = "045881"
        //    //};
        //    //var employee3 = new Employee
        //    //{
        //    //    CIN = "0068233",
        //    //    Mail = "nadra.msallem232@gmail.com",
        //    //    NumeroPersonne = "04588331"
        //    //};
        //    //listEmployee.Add(employee1);
        //    //listEmployee.Add(employee2);
        //    //listEmployee.Add(employee3);

        //    //var employeeToCreate = new EmployeeViewModel
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem14@gmail.com",
        //    //    NumeroPersonne = "000001030"
        //    //};

        //    //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        //    //var expected = "EmployeeExitCIN";
        //    //Assert.Equal(expected, actual);
        //}
        //[TestMethod]
        //public void EmployeeExitMail()
        //{
        //    //var setupService = new SetupServices();
        //    //var provider = setupService.SetupProvider();
        //    //var service = provider.GetService<IEmployeeServices>();
        //    //var listEmployee = new List<Employee>();
        //    //var employee1 = new Employee
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};
        //    //var employee2 = new Employee
        //    //{
        //    //    CIN = "006822222",
        //    //    Mail = "nadra.msallem22@gmail.com",
        //    //    NumeroPersonne = "045881"
        //    //};
        //    //var employee3 = new Employee
        //    //{
        //    //    CIN = "0068233",
        //    //    Mail = "nadra.msallem232@gmail.com",
        //    //    NumeroPersonne = "04588331"
        //    //};
        //    //listEmployee.Add(employee1);
        //    //listEmployee.Add(employee2);
        //    //listEmployee.Add(employee3);

        //    //var employeeToCreate = new EmployeeViewModel
        //    //{
        //    //    CIN = "0068469101",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001030"
        //    //};

        //    //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        //    //var expected = "EmployeeExitMail";
        //    //Assert.Equal(expected, actual);
        //}
        //[TestMethod]
        //public void EmployeeExitMatricule()
        //{
        //    //var setupService = new SetupServices();
        //    //var provider = setupService.SetupProvider();
        //    //var service = provider.GetService<IEmployeeServices>();
        //    //var listEmployee = new List<Employee>();
        //    //var employee1 = new Employee
        //    //{
        //    //    CIN = "00684691",
        //    //    Mail = "nadra.msallem@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};
        //    //var employee2 = new Employee
        //    //{
        //    //    CIN = "006822222",
        //    //    Mail = "nadra.msallem22@gmail.com",
        //    //    NumeroPersonne = "045881"
        //    //};
        //    //var employee3 = new Employee
        //    //{
        //    //    CIN = "0068233",
        //    //    Mail = "nadra.msallem232@gmail.com",
        //    //    NumeroPersonne = "04588331"
        //    //};
        //    //listEmployee.Add(employee1);
        //    //listEmployee.Add(employee2);
        //    //listEmployee.Add(employee3);

        //    //var employeeToCreate = new EmployeeViewModel
        //    //{
        //    //    CIN = "006846910",
        //    //    Mail = "nadra.msallem44@gmail.com",
        //    //    NumeroPersonne = "000001"
        //    //};

        //    //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
        //    //var expected = "EmployeeExitMatricule";
        //    //Assert.Equal(expected, actual);
        //}
        [TestMethod]
        public void EmployeeNotExit()
        {
            //var setupService = new SetupServices();
            //var provider = setupService.SetupProvider();
            //var service = provider.GetService<IEmployeeServices>();
            //var listEmployee = new List<Employee>();
            //var employee1 = new Employee
            //{
            //    CIN = "00684691",
            //    Mail = "nadra.msallem@gmail.com",
            //    NumeroPersonne = "000001"
            //};
            //var employee2 = new Employee
            //{
            //    CIN = "006822222",
            //    Mail = "nadra.msallem22@gmail.com",
            //    NumeroPersonne = "045881"
            //};
            //var employee3 = new Employee
            //{
            //    CIN = "0068233",
            //    Mail = "nadra.msallem232@gmail.com",
            //    NumeroPersonne = "04588331"
            //};
            //listEmployee.Add(employee1);
            //listEmployee.Add(employee2);
            //listEmployee.Add(employee3);

            //var employeeToCreate = new EmployeeViewModel
            //{
            //    CIN = "006846910",
            //    Mail = "nadra.msallem1@gmail.com",
            //    NumeroPersonne = "000001030"
            //};

            //var actual = service.CheckUnicityEmployeeParameterCreate(listEmployee, employeeToCreate);
            //var expected = "";
            //Assert.Equal(expected, actual);
        }
    }
}
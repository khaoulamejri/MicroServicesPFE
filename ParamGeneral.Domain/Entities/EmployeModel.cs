using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class EmployeModel
    {
        public string NumeroPersonne { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Employee MappingAddEmployee(EmployeeViewModel e1, Employee e2)
        {
        
            e2.NumeroPersonne = e1.NumeroPersonne;
            e2.Nom = e1.Nom;
            e2.Prenom = e1.Prenom;
            return e2;
        }
    }
}

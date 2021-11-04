﻿using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Repositories
{
   public  class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IEmployeeRepository : IRepository<Employee> { }

}


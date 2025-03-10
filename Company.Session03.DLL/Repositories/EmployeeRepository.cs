using Company.Session03.BLL.Interfaces;
using Company.Session03.DAL.Data.Contexts;
using Company.Session03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Session03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {


        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            
        }



    }
}

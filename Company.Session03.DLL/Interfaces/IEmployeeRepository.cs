using Company.Session03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Session03.BLL.Interfaces
{
   public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        Task<List<Employee>> GetByNameAsync(string name);
    }
}

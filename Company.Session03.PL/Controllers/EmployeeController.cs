using Company.Session03.BLL.Interfaces;
using Company.Session03.DAL.Models;
using Company.Session03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Session03.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees= _employeeRepository.GetAll();


            // Dictionary : 3 Property 
            // 1. ViewData : Transfer Extra Information From Controller (Action) To View

            //ViewData["Message"] = "Hello From View Data";




            // 2. ViewBag : Transfer Extra Information From Controller (Action) To View  

            //ViewBag.Message = "Hello From View Bag";





            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDtos model)
        {

            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    
                    Name = model.Name,
                    Address=model.Address,
                    Age=model.Age,
                    Email=model.Email,
                    HiringDate=model.HiringDate,
                    Phone=model.Phone,
                    Salary=model.Salary,
                    IsActive=model.IsActive,
                    IsDeleted=model.IsDeleted,
                    CreateAt = model.CreateAt,
                    DepartmentId=model.DepartmentId
                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Create!!!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }






        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id {id} is Not Found" });
            return View(employee);


        }




        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id {id} is Not Found" });

            var employeeDto = new CreateEmployeeDtos()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                CreateAt = employee.CreateAt
            };

            return View(employeeDto);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid)
            {
                //if (id != employee.Id) return BadRequest();
                
                    var employee = new Employee()
                    {
                        Id=id,
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        HiringDate = model.HiringDate,
                        Phone = model.Phone,
                        Salary = model.Salary,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        CreateAt = model.CreateAt
                    };

                    var count = _employeeRepository.Update(employee);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);


        }





        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id {id} is Not Found" });
            return View(employee);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();

                var count = _employeeRepository.Delete(employee);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);


        }







    }
}

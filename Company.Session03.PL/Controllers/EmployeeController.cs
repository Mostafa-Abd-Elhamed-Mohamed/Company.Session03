using AutoMapper;
using Company.Session03.BLL.Interfaces;
using Company.Session03.DAL.Models;
using Company.Session03.PL.Dtos;
using Company.Session03.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Session03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork ,
            IMapper mapper
            )
        {
           _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);

            }


            // Dictionary : 3 Property 
            // 1. ViewData : Transfer Extra Information From Controller (Action) To View

            //ViewData["Message"] = "Hello From View Data";




            // 2. ViewBag : Transfer Extra Information From Controller (Action) To View  

            //ViewBag.Message = "Hello From View Bag";





            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDtos model)
        {

            if (ModelState.IsValid)
            {

                if (model.Image is not null)
                {
                    model.ImgeName = DocumentSetting.UploadFile(model.Image, "images");
                }
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Create!!!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }






        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id {id} is Not Found" });
            return View(employee);


        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid)
            {
                //if (id != employee.Id) return BadRequest();
                
                if(model.ImgeName is not null && model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImgeName , "images");
                }   

                if(model.Image is not null)
                {
                    model.ImgeName = DocumentSetting.UploadFile(model.Image , "images");
                }
                
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
               
                     _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);


        }





        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id {id} is Not Found" });
            return View(employee);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();

                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (employee.ImgeName is not null)
                    {
                       DocumentSetting.DeleteFile(employee.ImgeName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);


        }







    }
}

using Company.Session03.DAL.Models;
using Company.Session03.PL.Dtos;
using Company.Session03.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Threading.Tasks;

namespace Company.Session03.PL.Controllers
{
    public class RoleController : Controller
    {



        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager ,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> role;
            if (string.IsNullOrEmpty(SearchInput))
            {
                role = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                  Name = U.Name
                });
            }
            else
            {
                role = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));

            }



            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);

                if(role is null)
                {
                    role = new IdentityRole() 
                    {
                      Name = model.Name
                    
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return NotFound(new { StatusCode = 404, message = $"Role With Id {id} is Not Found" });
            return View(role);


        }




        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {


            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);


            if (role is null) return NotFound(new { StatusCode = 404, message = $"Role With Id {id} is Not Found" });

            var roleToReturnDto = new RoleToReturnDto()
            {
                Id = role.Id,
               Name = role.Name
             
            };

            return View(roleToReturnDto);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");


                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Id"); 
                
               var roleResult = await _roleManager.FindByNameAsync(model.Name);

                if (roleResult is  null){


                    role.Name = model.Name;


                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        RedirectToAction(nameof(Index));
                    }
                }

                ModelState.AddModelError("","Invalid ");

            }
            return View(model);


        }





        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {



            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return NotFound(new { StatusCode = 404, message = $"User With Id {id} is Not Found" });

            return View(role);



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");


                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Id");


                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        RedirectToAction(nameof(Index));
                    }
                

                ModelState.AddModelError("", "Invalid ");

            }
            return View(model);



        }


        //[HttpGet]
        //public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        //{

        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role is null)
        //        return NotFound();

        //    var usersInRole = new List<UsersInRoleDto>();
        //    var users = await _userManager.Users.ToListAsync();

        //    foreach(var user in users)
        //    {
        //        var userInRole = new UsersInRoleDto()
        //        {
        //              UserId = user.Id,
        //              UserName = user.userName,
                      
        //        };

        //    }



        //}



    }
}

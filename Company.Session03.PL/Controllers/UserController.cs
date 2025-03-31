using Company.Session03.DAL.Models;
using Company.Session03.PL.Dtos;
using Company.Session03.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Session03.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    Email = U.Email,
                    FirstName = U.FristName,
                    LastName = U.LastName,
                    UserName = U.UserName,
                    Roles = _userManager.GetRolesAsync(U).Result
                });
            }
            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    Email = U.Email,
                    FirstName = U.FristName,
                    LastName = U.LastName,
                    UserName = U.UserName,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));

            }



            return View(users);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var users = await _userManager.FindByIdAsync(id);

            if (users is null) return NotFound(new { StatusCode = 404, message = $"User With Id {id} is Not Found" });
            return View(users);


        }




        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
  

            if (id is null) return BadRequest("Invalid Id");

            var user = await _userManager.FindByIdAsync(id);


            if (user is null) return NotFound(new { StatusCode = 404, message = $"User With Id {id} is Not Found" });

            var userToReturnDto = new UserToReturnDto()
            {
                Id = user.Id,
                FirstName = user.FristName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(userToReturnDto);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id !=model.Id) return BadRequest("Invalid Id");


                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return BadRequest("Invalid Id");

                user.UserName = model.UserName;
                user.FristName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    RedirectToAction(nameof(Index));
                }

            }
            return View(model);


        }





        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {



            if (id is null) return BadRequest("Invalid Id");

            var users = await _userManager.FindByIdAsync(id);

            if (users is null) return NotFound(new { StatusCode = 404, message = $"User With Id {id} is Not Found" });

            return View(users);



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");


                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return BadRequest("Invalid Id");

          

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    RedirectToAction(nameof(Index));
                }

            }
            return View(model);



        }




    }
}

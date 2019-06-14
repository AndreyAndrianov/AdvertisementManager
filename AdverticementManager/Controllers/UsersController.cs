using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdverticementManager.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdverticementManager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.FirstName,
                Surname = user.Surname,
                IsManager = userRoles.Contains(Role.User.ToString(), StringComparer.InvariantCultureIgnoreCase),
                IsAdmin = userRoles.Contains(Role.Admin.ToString(), StringComparer.InvariantCultureIgnoreCase)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FirstName = model.Name;
                    user.Surname = model.Surname;

                    var result = await _userManager.UpdateAsync(user);
                    if (model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
                    }

                    if (model.IsManager)
                    {
                        await _userManager.AddToRoleAsync(user, Role.User.ToString());
                    }
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RoleController : Controller
    {

        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public RoleController(RoleManager<IdentityRole>roleManager,ApplicationDbContext db , UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        public async Task<IActionResult>Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(string Name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.mgs = "This Role is aldeady exist";
                ViewBag.Name = Name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var role =await _roleManager.FindByIdAsync(Id);
            if (role==null)
            {
                return NotFound();
            }
            ViewBag.Id = role.Id;
            ViewBag.Name = role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Id,string Name)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = Name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.mgs = "This Role is aldeady exist";
                ViewBag.Name = Name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been uptated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.Id = role.Id;
            ViewBag.Name = role.Name;
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult>DeleteConfirm(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();
            }

            var result =await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult>Assign()
        {
            ViewData["UserId"] = new SelectList(_db.ApplicationUsers.Where(f=>f.LockoutEnd<DateTime.Now||f.LockoutEnd==null).ToList(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Assign(RoleUserVm roleUser)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == roleUser.UserId);
            var isCheckRoleAssign =await _userManager.IsInRoleAsync(user, roleUser.RoleId);
            if (isCheckRoleAssign)
            {
                ViewBag.mgs = "Rhis user already assign this role.";
                ViewData["UserId"] = new SelectList(_db.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }
            var role = await _userManager.AddToRoleAsync(user,roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "User Role assigned.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult AssignUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.ApplicationUsers on ur.UserId equals a.Id
                         select new UserRoleMaping()
                         {
                             UserId = ur.UserId,
                             RoleId=ur.RoleId,
                             UserName=a.UserName,
                             RoleName=r.Name
                         };
            ViewBag.UserRoles = result;
            return View();
        }
    }
}

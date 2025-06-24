using ASP.NET_MVC_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;

        public UsersController()
        {
            _context = new ApplicationContext();
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users createUserDto)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    Mobile = createUserDto.Mobile,
                    Address = createUserDto.Address,
                    City = createUserDto.City,
                    District = createUserDto.District,
                    State = createUserDto.State,
                    Comments = createUserDto.Comments
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "User Successfully Created!";
                return RedirectToAction("UserList");
            }

            return View("Adduser", createUserDto);
        }

        [HttpGet]
        public ActionResult UserList()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                user.Mobile = updatedUser.Mobile;
                user.Address = updatedUser.Address;
                user.City = updatedUser.City;
                user.District = updatedUser.District;
                user.State = updatedUser.State;
                user.Comments = updatedUser.Comments;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("UserList");
            }

            return View(updatedUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction("UserList");
        }
    }
}
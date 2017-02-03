using GradingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradingSystem.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            var role = CurrentUserRole();
            if (role == "Student")
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                return RedirectToAction("Index", "Teacher");
            }
            
        }

        public string CurrentUserRole()
        {


            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            var Teacher = UserManager.FindById(User.Identity.GetUserId());
            var roleID = Teacher.Roles;
            var role = roleManager.FindById(roleID.ToArray()[0].RoleId);

            return role.Name;

        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
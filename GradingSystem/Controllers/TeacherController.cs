using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GradingSystem.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        //public ApplicationDbContext db = new ApplicationDbContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// User manager - attached to application DB context
        protected UserManager<ApplicationUser> UserManager { get; set; }
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public TeacherController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public string CurrentUserName()
        {
            var Teacher = UserManager.FindById(User.Identity.GetUserId());
            string TeacherName = Teacher.UserName;
            return TeacherName;
        }

        public string CurrentUserRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            var Teacher = UserManager.FindById(User.Identity.GetUserId());
            var roleID = Teacher.Roles;
            var role = roleManager.FindById(roleID.ToArray()[0].RoleId);
            return role.Name;
        }

        public string GetStudentID(string name)
        {
           if(name != null)
            {
                ApplicationDbContext = new ApplicationDbContext();
                UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));

                var Student = UserManager.FindByName(name);
                var StudentID = Student.Id;
                return StudentID;
            }
            else
            {
                return null;
            }

        }
        
        //[Authorize(Roles ="Teacher")]
        // GET: Teacher 
        public ActionResult Index()
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            //var context = new ApplicationDbContext();
            string TeacherName = CurrentUserName();
            //get all student list
            var allUsers = dbcontext.Users.ToList();
            List<string> Studentlist = new List<string>();
            foreach (var u in allUsers)
            {
                var roleID = u.Roles;
                var role = roleManager.FindById(roleID.ToArray()[0].RoleId);
                if (role.Name == "Student")
                {
                    Studentlist.Add(u.UserName);
                }

            }
            ViewData["passedArray"] = Studentlist.ToArray();
            ViewBag.TeacherName = TeacherName;
            return View();

        }

        //[Authorize(Roles = "Teacher")]
        [HttpGet]
        public ViewResult Mark(string name)
        {
            //get marked student id by name
            string StudentId = GetStudentID(name);
            ViewBag.StudentName = name;
            //get marked homework by the passed name
            Homework homework = dbcontext.HomeworkDB.FindbyStudentID(StudentId);
            //var filePath = homework.HomeworkUrl;
            if (homework != null )
            {
                //ViewBag.flag = null;
                ViewBag.HomeworkId = homework.HomeworkID;
            }

            //if((homework.Feedback != null) && (homework.Grade != null))
            //{
            //    ViewBag.isMarked = "1";
            //}
            //else
            //{
            //    ViewBag.isMarked = null;
            //}

            //CurrentHmID = homeworks[0].HomeworkID;
            return View(homework);
        }
        
        //[Authorize(Roles = 'Teacher')]
        [HttpPost]
        public ActionResult Mark(int hmid,Homework hm)
        {
            Homework current = dbcontext.HomeworkDB.FindbyHomeworkID(hmid);
            using (var dbContextTransaction = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    current.Feedback = hm.Feedback;
                    current.Grade = hm.Grade;
                    dbcontext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch(Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return RedirectToAction("MarkSuccess", hm);//return redirection
        }
        //redirection view
        [Route("Teacher/MarkSuccess")]
        public ViewResult MarkSuccess(Homework hm)
        {
            return View(hm);
        }
    }
}
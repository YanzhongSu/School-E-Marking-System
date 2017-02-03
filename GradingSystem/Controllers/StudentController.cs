using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GradingSystem.Controllers
{

    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        /// Application DB context
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// User manager - attached to application DB context
        protected UserManager<ApplicationUser> UserManager { get; set; }
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public StudentController()
        {

            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));

        }
        public string CurrentUserID()
        {


            //this.ApplicationDbContext = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));

            var Student = UserManager.FindById(User.Identity.GetUserId());
            StudentID = Student.Id;

            return StudentID;
        }

        public string CurrentUserName()
        {
            //this.ApplicationDbContext = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));

            var Student = UserManager.FindById(User.Identity.GetUserId());
            string StudentName = Student.UserName;

            return StudentName;

        }
        public string StudentID { get; set; }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UserName = CurrentUserName();
            string sID = CurrentUserID();
            Homework homeworks = dbcontext.HomeworkDB.FindbyStudentID(sID);
            if (homeworks == null)
            {
                ViewBag.flag = null;
            }
            else
            {
                ViewBag.flag = "1";
            }
            return View(homeworks);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase SelectedHomework)
        {
            //Upload homework file
            ViewBag.UserName = CurrentUserName();
            if (SelectedHomework != null)
            {
                var path = Path.Combine("C:/Users/PlayGame/Documents/Data/",
                                        Path.GetFileName(SelectedHomework.FileName));
                SelectedHomework.SaveAs(path);
                Homework hm = new Homework {
                    StudentID = CurrentUserID(),
                    HomeworkData = new byte[SelectedHomework.ContentLength],
                    //Feedback = "null",
                    //Grade = Grade.F,
                    HomeworkType = SelectedHomework.ContentType,
                    //delete the path
                    HomeworkUrl = path,
                    UploadDate = DateTime.Now
                };
                SelectedHomework.InputStream.Read(hm.HomeworkData, 0, hm.HomeworkData.Length);
                using (var dbContextTransaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {                        
                        dbcontext.HomeworkDB.Add(hm);
                        dbcontext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch(Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            //if uploaded successfully, then read it from database
            Homework homeworks = dbcontext.HomeworkDB.FindbyStudentID(CurrentUserID());
            //var homeworks = context.HomeworkDB.FindbyStudentID(sID);
            if (homeworks == null)
            {
                ViewBag.flag = null;
            }
            else
            {
                ViewBag.flag = "1";
            }
            return View("Index", homeworks);
        }
    }
}
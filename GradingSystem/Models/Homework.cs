using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GradingSystem.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradingSystem.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }


    public class Homework
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int HomeworkID { get; set; }
        [Required]
        //[ForeignKey("StudentID")]
        [HiddenInput(DisplayValue = false)]
        public string StudentID { get; set; }
        

        public byte[] HomeworkData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string HomeworkType { get; set; }
        public Grade? Grade { get; set; }
        [DataType(DataType.MultilineText)]
        public string Feedback { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string HomeworkUrl { get; set;}
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        //public string Test { get; set; }

    //public virtual ApplicationUser Users { get; set; } // to be continue
}

    //public class HomeworkContext: DbContext
    //{
    //    public HomeworkContext(): base("DefaultConnection")
    //    {
    //       Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, GradingSystem.Migrations.Configuration>("DefaultConnection"));
    //    }

    //    public DbSet<Homework> HomeworkDB { get; set; }

    //    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    //{
    //    //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    //    //}
    //}

    public static class MoreExtensionMethods
    {

        public static Homework FindbyStudentID(this IEnumerable<Homework> homeworks, string sID)
        {
            var hw = (from i in homeworks where i.StudentID == sID select i);
            var hw_temp = hw;
            if (hw_temp.ToArray().Length == 0)
            {
                return null;
            }
            else
            {
                return hw.First();
                
            }
            //return (from i in homeworks where i.StudentID == sID select i).First();
        }
        public static Homework FindbyHomeworkID(this IEnumerable<Homework> homeworks, int hID)
        {
            //return (from i in homeworks where i.HomeworkID == hID select i).First();
            var hw = (from i in homeworks where i.HomeworkID == hID select i);
            var hw_temp = hw;
            if (hw_temp.ToArray().Length == 0)
            {
                return null;
            }
            else
            {
                return hw.First();

            }
        }
    }

}
using GradingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradingSystem.Startup))]
namespace GradingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        //private void createRolesandUsers()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    if (!roleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin rool   
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                  

        //        var adminuser = new ApplicationUser();
        //        adminuser.UserName = "shanu";
        //        adminuser.Email = "syedshanumcain@gmail.com";

        //        string adminuserPWD = "A@Z200711";

        //        var adminchkUser = UserManager.Create(adminuser, adminuserPWD);

        //        //Add default User to Role Admin   
        //        if (adminchkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(adminuser.Id, "Admin");

        //        }
        //    }
        //    // creating Creating Manager role    
        //    if (!roleManager.RoleExists("Teacher"))
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "Teacher";
        //        roleManager.Create(role);

        //    }

        //    // creating Creating Employee role    
        //    if (!roleManager.RoleExists("Student"))
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "Student";
        //        roleManager.Create(role);

        //    }

        //    var user = new ApplicationUser();
        //    user.UserName = "baiyang";
        //    user.Email = "by@gmail.com";

        //    string userPWD = "Abcd1234.";

        //    var chkUser = UserManager.Create(user, userPWD);

        //    //Add default User to Role Admin   
        //    if (chkUser.Succeeded)
        //    {
        //        var result1 = UserManager.AddToRole(user.Id, "Student");

        //    }
        //}
    }
}
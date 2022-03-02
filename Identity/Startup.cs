using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identity.Startup))]
namespace Identity
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateUsers();
        }
        public void CreateUsers()
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "Moussa@yahoo.com";
            user.UserName = "Moussa";
            var check = usermanager.Create(user, "Mou$$@123");
            if (check.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Admins");
            }
        }
        public void CreateRoles()
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!rolemanager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                rolemanager.Create(role);
            }
            if (!rolemanager.RoleExists("Others"))
            {
                role = new IdentityRole();
                role.Name = "Others";
                rolemanager.Create(role);
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using MyLeaveManagement.Data;

namespace MyLeaveManagement
{
    public static class SeedData
    {
        public  async static Task SeedAsync(UserManager<Employee> userManager,RoleManager<IdentityRole>roleManager)
        {
            await SeedRole(roleManager);
            await SeedUsers(userManager);
        }
        private static async Task SeedUsers(UserManager<Employee> userManager)
        {
            if(await userManager.FindByNameAsync("admin@localhost.com") == null)
            {
                var admin = new Employee
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                };
                var result= await userManager.CreateAsync(admin,"1q2w3e4rA!");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    
                }
            }
        }
        private static async Task SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
               var result=await roleManager.CreateAsync(role);
            }
                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    var user = new IdentityRole
                    {
                        Name = "Employee"
                    };
                var result = await roleManager.CreateAsync(user);
                }
            }

    }
}

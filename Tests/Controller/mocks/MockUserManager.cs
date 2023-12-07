using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tests.Controller.mocks
{
    public class MockUserManager
    {

        public static Mock<UserManager<Employee>> GetMock()
        {
            var _Users = new List<Employee>()
            {
                new Employee()
                {
                    Id = "a3f",
                    DateJoined = Convert.ToDateTime("02.12.2023 1:32:57"),
                    Email = "request@gmail.com",
                    UserName = "request@gmail.com",
                    FirstName = "firstrequest",
                    LastName = "lastrequest",

                }
            };
            //id-key
            var roles = new List<KeyValuePair<string,string>>
            {
                new KeyValuePair<string,string>( "a3f", "Employee"),  
                new KeyValuePair<string,string>("a4f", "Employee"),
                new KeyValuePair<string,string>("a3f", "Admin"),
                
            };
            var mock = new Mock<UserManager<Employee>>(
                       new Mock<IUserStore<Employee>>().Object,
                       new Mock<IOptions<IdentityOptions>>().Object,
                       new Mock<IPasswordHasher<Employee>>().Object,
                       new IUserValidator<Employee>[0],
                       new IPasswordValidator<Employee>[0],
                       new Mock<ILookupNormalizer>().Object,
                       new Mock<IdentityErrorDescriber>().Object,
                       new Mock<IServiceProvider>().Object,
                       new Mock<ILogger<UserManager<Employee>>>().Object
                   );
            mock.Object.UserValidators.Add(new UserValidator<Employee>());
            mock.Object.PasswordValidators.Add(new PasswordValidator<Employee>());



            mock.Setup(m => m.DeleteAsync(It.IsAny<Employee>())).ReturnsAsync(IdentityResult.Success);
            mock.Setup(m => m.CreateAsync(It.IsAny<Employee>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback((Employee e, string p) => _Users.Add(e));
            mock.Setup(m => m.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync(IdentityResult.Success);
            mock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new Employee()
                {
                    Id = "ba9581c0",
                    DateJoined = Convert.ToDateTime("02.12.2023 1:32:57"),
                    Email = "request@gmail.com",
                    UserName = "request@gmail.com",
                    FirstName = "firstrequest",
                    LastName = "lastrequest"
                });
            mock.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string T) => _Users.FirstOrDefault(a => a.Id == T));
            mock.Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string userName) => _Users.FirstOrDefault(e => e.UserName == userName));
            mock.Setup(m => m.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync((string T) => _Users.Where(a => a.Id == roles.FirstOrDefault(q => q.Value == T).Key).ToList());
            return mock;
        }
    }
}

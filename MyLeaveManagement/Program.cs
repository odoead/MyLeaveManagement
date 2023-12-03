using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyLeaveManagement;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Mappings;
using MyLeaveManagement.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
#region add DI injection of controllers
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
#endregion
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(Maps));
builder.Services.AddDefaultIdentity<Employee>(/*options => { options.SignIn.RequireConfirmedAccount = false; options.Password =}*/)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
Console.WriteLine("----------------------------------------------");
foreach (var svs in builder.Services)
{
    Console.WriteLine(svs.ServiceType.FullName +"\n");
}
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
        var userManager = services.GetRequiredService<UserManager<Employee>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
         await SeedData.SeedAsync(userManager, roleManager);
   
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

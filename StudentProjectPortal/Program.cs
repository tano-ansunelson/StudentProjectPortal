using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;
using StudentProjectPortal.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(Options =>
{
    Options.Password.RequireDigit = true;
    Options.Password.RequiredLength = 8;
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequireUppercase = true;
    Options.Password.RequireLowercase = true;
}   ).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();


builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISupervisorRepository, SupervisorRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan=TimeSpan.FromMinutes(30);
    options.SlidingExpiration=true;
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
/* 
 * using (var scope = app.Services.CreateScope())
 * {
 * var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
 * string[] roles = { "Supervisor", "Student" };
 * 
 * foreach (var role in roles)
 * {
 * if (!await roleManager.RoleExistsAsync(role))
 * {
 * await roleManager.CreateAsync(new IdentityRole(role));
 * }
 * 
 * }
 * 
 * 
 * 
 * }
 */

using (var scope = app.Services.CreateScope()){
    await SeedData.SeedRoles(scope.ServiceProvider);

}



app.Run();


using BloggingSite.Models.Entities;
using BlogginSite.Repositories.SeedData;
using BloggingSite.Services.IService;
using BloggingSite.Services.Service;
using BlogginSite.Repositories.Db;
using BlogginSite.Repositories.IRepository;
using BlogginSite.Repositories.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloggingSite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var conn = builder.Configuration.GetConnectionString("BlogApplication");
            builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(conn));

            builder.Services.AddIdentity<MyUser, IdentityRole<long>>(optn =>
                            {
                                optn.Password.RequiredUniqueChars = 0;
                                optn.Password.RequiredLength = 6;
                                optn.Password.RequireUppercase = false;
                                optn.Password.RequireNonAlphanumeric = false;
                                optn.Password.RequireLowercase = false;
                                optn.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
                            })
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            
            builder.Services.AddScoped<IPendingBlogService,PendingBlogService>();

            builder.Services.AddScoped<IApprovedBlogRepository,ApprovedBlogRepository>();
            
            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<UserManager<MyUser>>();
                await UserSeedData.Initialize(services);
            }

            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.MapStaticAssets();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

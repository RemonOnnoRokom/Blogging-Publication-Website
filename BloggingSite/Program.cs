
using BloggingSite.Services.IService;
using BloggingSite.Services.Service;
using BlogginSite.Repositories.Db;
using BlogginSite.Repositories.IRepository;
using BlogginSite.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var conn = builder.Configuration.GetConnectionString("BlogApplication");
            builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(conn));

            builder.Services.AddScoped<IApprovedBlogService,ApprovedBlogService>();

            builder.Services.AddScoped<IApprovedBlogRepository,ApprovedBlogRepository>();

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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

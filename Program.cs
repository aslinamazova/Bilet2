using Bizland.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bizland
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>
                (opt=>
                {
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireNonAlphanumeric = true;

                    opt.User.RequireUniqueEmail = true;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                });


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();

            });

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using TestTaskMatveew.DAL.Context;
using TestTaskMatveew.Services;
using TestTaskMatveew.Services.Interfaces;

namespace TestTaskMatveew
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TestTaskMatveewDB>(opt
                     => opt.UseSqlServer(config["ConnectionStrings:SqlServer"]));
            builder.Services.AddScoped<IXmlOffer, SqlXmlOffer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
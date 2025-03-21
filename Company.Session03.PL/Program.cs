using Company.Session03.BLL;
using Company.Session03.BLL.Interfaces;
using Company.Session03.BLL.Repositories;
using Company.Session03.DAL.Data.Contexts;
using Company.Session03.PL.Mapping;
using Company.Session03.PL.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.Session03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddAutoMapper(typeof(EmployeeProfile));

            //builder.Services.AddScoped();
            //builder.Services.AddTransient();
            //builder.Services.AddSingleton();

            ////builder.Services.AddScoped<IScopedService,ScopedService>();
            //builder.Services.AddTransient<ITarnsentService, TarnsentService>();
            //builder.Services.AddSingleton<ISengeltonService, SengeltonService>();






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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

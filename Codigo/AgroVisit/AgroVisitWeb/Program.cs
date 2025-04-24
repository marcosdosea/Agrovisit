using AgroVisitWeb.Areas.Identity.Data;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service;

namespace AgroVisitWeb
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddUserSecrets<SecretReference>()
                .AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IIntervencaoService, IntervencaoService>();
            builder.Services.AddTransient<IProjetoService, ProjetoService>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<IPropriedadeService, PropriedadeService>();
            builder.Services.AddTransient<IVisitaService, VisitaService>();
            builder.Services.AddTransient<IPlanoService, PlanoService>();
            builder.Services.AddTransient<ICulturaService, CulturaService>();
            builder.Services.AddTransient<ISoloService, SoloService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<AgroVisitContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("AgroVisitDatabase")));

            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("AgroVisitDatabase")));

            var connectionString = builder.Configuration.GetConnectionString("AgroVisitDatabase");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'AgroVisitDatabase' está ausente.");
            }

            builder.Services.AddDefaultIdentity<UsuarioIdentity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "AgroVisitCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Service;
using Microsoft.AspNetCore.Identity;
using AgroVisitWeb.Areas.Identity.Data;

namespace AgroVisitWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IIntervencaoService, IntervencaoService>();
            builder.Services.AddTransient<IProjetoService, ProjetoService>();
            builder.Services.AddTransient<IIntervencaoService, IntervencaoService>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<IPropriedadeService, PropriedadeService>();
            builder.Services.AddTransient<IVisitaService, VisitaService>();
            builder.Services.AddTransient<ICulturaService, CulturaService>();
            builder.Services.AddTransient<ISoloService, SoloService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<AgroVisitContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AgroVisitDatabase")));

            builder.Services.AddDbContext<IdentityContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AgroVisitDatabase")));

            builder.Services.AddDefaultIdentity<UsuarioIdentity>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();
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
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
    
}
    

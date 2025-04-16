using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Service;

namespace AgroVisitAPI
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IClienteService, ClienteService>();

            builder.Services.AddTransient<IProjetoService, ProjetoService>();

            builder.Services.AddTransient<IVisitaService, VisitaService>();

            builder.Services.AddTransient<IIntervencaoService, IntervencaoService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<AgroVisitContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AgroVisitDatabase")));

            var connectionString = builder.Configuration.GetConnectionString("AgroVisitDatabase");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("A String de conexão 'AgroVisitDatabase' está ausente. Verifique o Secret Manager.");
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
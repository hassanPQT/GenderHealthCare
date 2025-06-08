
using BusinessAccess.Services.Implements;
using BusinessAccess.Services.Interfaces;
using DataAccess.DBContext;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;
using GenderHealcareSystem.CustomActionFilters;
using Microsoft.EntityFrameworkCore;

namespace GenderHealcareSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Inject Repositories
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            // Inject Services
            builder.Services.AddScoped<IServiceService, ServiceService>();

            //Add AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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

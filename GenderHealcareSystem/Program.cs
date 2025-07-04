using BusinessAccess.Services;
using BusinessAccess.Services.Implements;
using BusinessAccess.Services.Interfaces;
using DataAccess.DBContext;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;
using GenderHealcareSystem.Converters;
using GenderHealcareSystem.CustomActionFilters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Respository.IRepositories;
using Respository.Repositories;

namespace GenderHealcareSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<AppDbContext>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //add scoped services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie().AddGoogle((options) =>
            {
                var clientId = builder.Configuration["Authentication:Google:ClientId"];
                if (clientId == null)
                {
                    throw new ArgumentNullException(nameof(clientId));
                }
                var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                if (clientSecret == null)
                {
                    throw new ArgumentNullException(nameof(clientSecret));
                }

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))

                            };


                        });
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IMenstrualCycleService, MenstrualCycleService>();
            // add scoped repositories
            builder.Services.AddScoped<IUserRespository, UserRespository>();
            builder.Services.AddScoped<IMenstrualCycleRespository, MenstrualCycleRespository>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "InputToken: Bearer : Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {  }
                    }
                });
            });
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5173") // đổi thành domain FE 
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials(); // nếu dùng cookie/token
                                  });
            });
            builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Inject Repositories
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IStaffConsultantRepository, StaffConsultantRepository>();
            builder.Services.AddScoped<IStaffScheduleRepository, StaffScheduleRepository>();


            // Inject Services
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IStaffConsultantService, StaffConsultantService>();
            builder.Services.AddScoped<IStaffScheduleService, StaffScheduleService>();



            //Add AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // convert date time
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

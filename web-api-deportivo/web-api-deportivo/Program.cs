
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using web_api_deportivo.Conection;
    using web_api_deportivo.IRepository;
    using web_api_deportivo.Repository;
    using web_api_deportivo.Validator;
    using web_api_deportivo.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

namespace web_api_deportivo
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //options.UseNpgsql(connectionString));

            builder.Services.AddDbContext<AppDbContext>(options =>
            options
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

            builder.Services.AddScoped<JwtService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlazorCorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7232", "http://localhost:5175")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
                builder.Services.AddScoped<IPermisosRepository, PermisosRepository>();
            builder.Services.AddSingleton<PasswordService>();
            builder.Services.AddValidatorsFromAssemblyContaining<RolValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
                        ),

                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization();


            builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
            app.UseCors("BlazorCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
        }
    }

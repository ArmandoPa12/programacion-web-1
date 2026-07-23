
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
    using Microsoft.OpenApi.Models;

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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors());

            builder.Services.AddScoped<JwtService>();

            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlazorCorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7232", "http://localhost:5175")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Repositorios y Servicios
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IPermisosRepository, PermisosRepository>();
            builder.Services.AddSingleton<PasswordService>();
            builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            builder.Services.AddScoped<IInfraestructuraRepository, InfraestructuraRepository>();
            builder.Services.AddScoped<ITallerRepository, TallerRepository>();
            builder.Services.AddScoped<IGrupoTallerRepository, GrupoTallerRepository>();

            // Validadores de FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<RolValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AlumnoValidator>();
            builder.Services.AddScoped<InfraestructuraValidator>();
            builder.Services.AddScoped<TallerValidator>();
            builder.Services.AddScoped<GrupoTallerValidator>();


            // Configuración de Autenticación con JWT
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
            builder.Services.AddEndpointsApiExplorer();

            // ◄ CONFIGURACIÓN DE SWAGGER CON BOTÓN PARA COPIAR TU TOKEN JWT
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API Deportivo", Version = "v1" });

                // Configurar la definición de seguridad de JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticación JWT usando el esquema Bearer. Ejemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Forzar a que Swagger use la seguridad JWT en los endpoints que la requieran
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("BlazorCorsPolicy");

            // ¡El orden del pipeline es perfecto aquí!
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        }
    }

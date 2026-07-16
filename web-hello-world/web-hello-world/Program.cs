
namespace web_hello_world;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using web_hello_world.Conection;
using web_hello_world.Repository;
using web_hello_world.Validators;
using FluentValidation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);      

        builder.AddServiceDefaults();

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

        // aqui registramos el repositorio de usuario
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // aqui registramos el validador de usuario
        builder.Services.AddValidatorsFromAssemblyContaining<UsersValidator>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

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

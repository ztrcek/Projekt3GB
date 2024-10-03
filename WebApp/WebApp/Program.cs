using Microsoft.EntityFrameworkCore;

namespace WebApp
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

            //-
            var connectionString = builder.Configuration.GetConnectionString("PodatkiConn");
            builder.Services.AddDbContext<PodatkiDb>(options => options.UseSqlServer(connectionString));

            //-


            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            Uporabnik newUser = new Uporabnik();
            newUser.id = 1;

            Console.Write(newUser.id);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

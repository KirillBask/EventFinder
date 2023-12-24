
using EventFinder.Data;
using EventFinder.Services;
using EventFinder.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventFinder
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

            builder.Services.AddTransient<IEventService, EventService>();
            builder.Services.AddTransient<DatabaseContext>(provider =>
            {
                var configuration = builder.Configuration.GetConnectionString("MongoDB");
                return new DatabaseContext(configuration, "EventFinder-dev-db");
            });


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
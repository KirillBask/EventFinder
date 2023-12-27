
using EventFinder.Data;
using EventFinder.Models;
using EventFinder.Services;
using EventFinder.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

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
            
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<DatabaseContext>(provider =>
            {
                var configuration = builder.Configuration.GetConnectionString("MongoDB");
                return new DatabaseContext(configuration, "EventFinder-dev-db");
            });

            // <Model.cs> - "CollectionName in db"
            RegisterMongoCollections<User>(builder, "Users");
            RegisterMongoCollections<Event>(builder, "Events");

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

        private static void RegisterMongoCollections<T>(WebApplicationBuilder builder, string collectionName)
        {
            builder.Services.AddScoped<IMongoCollection<T>>(provider =>
            {
                var context = provider.GetRequiredService<DatabaseContext>();
                var property = typeof(DatabaseContext).GetProperty(collectionName);
                if (property == null)
                {
                    throw new InvalidOperationException($"Property {collectionName} not found in DatabaseContext");
                }
                return (IMongoCollection<T>)property.GetValue(context);
            });
        }
    }
}
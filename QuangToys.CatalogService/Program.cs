using QuangToys.CatalogService.Interfaces;
using QuangToys.CatalogService.Services;
using QuangToys.Common.Models;

namespace QuangToys.CatalogService
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
            builder.Services.Configure<CosmosSettings>(builder.Configuration.GetSection("CosmosDb"));
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();


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

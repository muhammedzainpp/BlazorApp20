
using System.Web.Services.Description;

namespace BlazorApp20.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        var app = builder.Build();
        app.UseCors("CorsPolicy");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
           
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseStaticFiles();

        app.Run();
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using UserManagementAPI_TechHiveSolutionsLab.Middleware;

namespace UserManagementAPI_TechHiveSolutionsLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddLogging();

            var app = builder.Build();

            app.UseMiddleware<RequestResponseLogging>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();
            app.Run();
        }
    }
}
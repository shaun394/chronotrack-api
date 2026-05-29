using ChronoTrack.Api.Extensions;
using ChronoTrack.Application;
using ChronoTrack.Infrastructure;

namespace ChronoTrack.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApi();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            app.UseApiPipeline();

            app.Run();
        }
    }
}
using ChronoTrack.Api.Middleware;

namespace ChronoTrack.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
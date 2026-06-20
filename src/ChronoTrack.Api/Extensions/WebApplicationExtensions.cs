namespace ChronoTrack.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        private const string FrontendCorsPolicy = "Frontend";

        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            app.UseMiddleware<Middleware.GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "ChronoTrack API v1");
                });
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(FrontendCorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
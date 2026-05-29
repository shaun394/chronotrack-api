namespace ChronoTrack.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();

            return services;
        }
    }
}
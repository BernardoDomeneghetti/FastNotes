using FastNotes.Api.Validators;

namespace FastNotes.Api.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddValidatorsService(this IServiceCollection services)
        {
            services.AddSingleton<UserValidator>();
            services.AddSingleton<LoginValidator>();
        }
    }
}

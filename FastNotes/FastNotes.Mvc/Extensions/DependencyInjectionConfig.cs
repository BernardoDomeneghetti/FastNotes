using FastNotes.Mvc.Validators;

namespace FastNotes.Mvc.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureValidatorsDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<LoginValidator>();
        }
    }
}

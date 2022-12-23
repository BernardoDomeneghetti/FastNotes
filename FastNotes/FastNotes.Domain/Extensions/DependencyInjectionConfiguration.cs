using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using FastNotes.Domain.Repositories;
using FastNotes.Domain.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FastNotes.Domain.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddFastNotesWorkerService(this IServiceCollection services, string ConnectionString)
        {
            #region EntityFramework
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            #endregion

            #region Repositories
            services.AddScoped<INoteFileRepository, NoteFileRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region Workers
            services.AddScoped<IUserWorker, UserWorker>();
            services.AddScoped<INoteWorker, NoteWorker>();
            services.AddScoped<ICategoryWorker, CategoryWorker>();
            #endregion
        }
    }
}

using RegEstudiantes.DataContext;
using RegEstudiantes.Interfaces;
using RegEstudiantes.Repository;
using RegEstudiantes.Services;

namespace RegEstudiantes.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDapper(this IServiceCollection services)
        {
            // 1. Configuración del contexto de base de datos
            services.AddSingleton<DapperContext>();

            // 2. Registro de repositorios
            services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            services.AddScoped<IMateriaRepository, MateriaRepository>();
            services.AddScoped<IProfesorRepository, ProfesorRepository>();

            // 3. Registro de servicios
            services.AddScoped<IEstudianteService, EstudianteService>();
            services.AddScoped<IMateriaService, MateriaService>();
            services.AddScoped<IProfesorService, ProfesorService>();
        }
    }
}

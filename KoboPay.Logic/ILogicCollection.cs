using KoboPay.Data;
using KoboPay.Logic.Implementations;
using KoboPay.Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoboPay.Logic
{
    public static class ILogicCollection
    {
        public static IServiceCollection RegisterAppService(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ICourseService, CourseService>();

            return services;
        }
    }
}

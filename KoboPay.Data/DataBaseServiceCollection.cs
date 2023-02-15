using KoboPay.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoboPay.Data
{
    public static class DataBaseServiceCollection
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection serviceDescriptors, IConfiguration confirguration)
        {
            serviceDescriptors.AddDbContext<DataContext>(options =>
            options.UseSqlServer(confirguration.GetConnectionString("KoboPayConn"), x =>
            {
                x.MigrationsAssembly("KoboPay.Data");
            }),ServiceLifetime.Scoped);

            return serviceDescriptors;
        }
    }
}

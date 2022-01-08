using Catalog.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API
{
    public static class StartupExtensions
    {
        public static void UseSeedData(this IApplicationBuilder app)
        {
            ICatalogDbContext context = app.ApplicationServices.GetRequiredService<ICatalogDbContext>();
            context.SeedData();
        }
    }
}

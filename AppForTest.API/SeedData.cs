using AppForTest.Domain.Entities;
using AppForTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppForTest.API
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<AppForTestDbContext>();
            context.Database.Migrate();

            if (!context.Countries.Any())
            {
                context.Countries.AddRange(
                    new Country
                    {
                        Name = "Russia",
                        Provinces = new List<Province>
                        {
                        new() { Name = "Moscow Oblast" },
                        new() { Name = "Krasnodar Krai" },
                        new() { Name = "Sverdlovsk Oblast" },
                        new() { Name = "Novosibirsk Oblast" },
                        new() { Name = "Krasnoyarsk Krai" },
                        new() { Name = "Irkutsk Oblast" },
                        new() { Name = "Chelyabinsk Oblast" }
                        }
                    },
                    new Country
                    {
                        Name = "Mexico",
                        Provinces = new List<Province>
                        {
                        new() { Name = "State of Mexico" },
                        new() { Name = "Jalisco" },
                        new() { Name = "Puebla" },
                        new() { Name = "Guanajuato" },
                        new() { Name = "Veracruz" },
                        new() { Name = "Chiapas" },
                        new() { Name = "Michoacán" }
                        }
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}


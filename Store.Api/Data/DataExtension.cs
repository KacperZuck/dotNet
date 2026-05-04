using System;
using Microsoft.EntityFrameworkCore;

namespace Store.Api.Data;

public static class DataExtension
{
    public static void MigrateDb( this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();  
    } 

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStore");

        // czas trawania scoped service dla pojedynczego zapytania dla dbContext:
        // tworzenie nowego dbContext per request , 
        // db jest droga wiec redukujemy nowe instacje przez kontrole zapytan
        // tworzenie w jedym miejscu wspiera wielowątkowość przez zabezpieczanie rywalizacji 
        // 
        
        builder.Services.AddScoped<GameStoreContext>();
        builder.Services.AddSqlite<GameStoreContext>(connString,optionsAction: options => options.UseSeeding((context, _ ) =>
        {

            if (!context.Set<Store.Api.Models.Type>().Any())
            {
                context.Set<Store.Api.Models.Type>().AddRange(
                    new Store.Api.Models.Type {name ="Nowy typ 1"},
                    new Store.Api.Models.Type {name ="Nowy typ 2"},
                    new Store.Api.Models.Type {name ="Nowy typ 3"},
                    new Store.Api.Models.Type {name ="Nowy typ 4"},
                    new Store.Api.Models.Type {name ="Nowy typ 5"}
                );
                context.SaveChanges();
            }
        }));
    }
}

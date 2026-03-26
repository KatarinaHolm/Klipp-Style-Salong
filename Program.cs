using Klipp_StyleSalong.Data;
using Klipp_StyleSalong.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using System;

namespace Klipp_StyleSalong
{
    public class Program
    {
        // Frågor:
        // Telefonnummer attributet kollade inte att det var tillräckligt många siffror, kan man göra det på nåt sätt? 
        // Är det korrekt att ta emot id för att ta bort bokning?
        // Vad ska man skriva i en ReadMe och hur strukturerar man den?
        // I vilket steg ska man ta bort appsettings.json och skapa repo i GitHub? Blev problem då jag hade skrivit in ConnectionString och skapade repo lokalt.
        // Bör man lägga till nån kontroll för skip/take eller ha med andra parametrar för paginering?
        // *AI-förslag : Sätt ett tak: Begränsa alltid pageSize (t.ex. max 100) så att en användare inte kan krascha servern genom att be om en miljon rader.
        // *AI-förslag: Returnera Metadata: Skicka med information i svaret om hur många sidor som finns totalt, så att frontend vet hur många knappar de ska rita ut.

        public static void Main(string[] args)
        {           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<SalonDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            BookingEndpoints.RegisterEndpoints(app);

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SalonDbContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}

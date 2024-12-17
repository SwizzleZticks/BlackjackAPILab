
using BlackJackLab.Services;

namespace BlackJackLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add HttpClient to the DI container for BlackjackService
            builder.Services.AddHttpClient<BlackjackService>(client =>
            {
                // Specify the base address for the HTTP client
                client.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck");
            });

            // Register the BlackjackService
            builder.Services.AddScoped<BlackjackService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}

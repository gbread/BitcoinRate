using BitcoinRateWeb.Components;
using BitcoinRateWeb.DAL;
using BitcoinRateWeb.Middleware;
using BitcoinRateWeb.Services;
using BitcoinRateWeb.Settings;
using Microsoft.EntityFrameworkCore;

namespace BitcoinRateWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            services.Configure<BitcoinSettings>(builder.Configuration.GetSection("BitcoinSettings"));
            services.AddSingleton<BitcoinService>();
            services.AddSingleton<CurrencyConversionProviderViaCnbService>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<BitcoinDataService>();
            services.AddHostedService(provider => provider.GetRequiredService<BitcoinDataService>());
            services.AddScoped<BitcoinStoredPriceService>();

            // Configure the DbContext for Entity Framework
            services.AddDbContext<BitcoinDataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
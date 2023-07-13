using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TextAnalyzerBot.Configuration;
using TextAnalyzerBot.Controllers;
using TextAnalyzerBot.Services;

namespace TextAnalyzerBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime() 
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddTransient<ITextAnalyzer,  TextAnalyzer>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6394869662:AAHPn8QdMIEG-rekhVZJ2Fik-JZXCLRMN8U"
            };
        }
    }
}


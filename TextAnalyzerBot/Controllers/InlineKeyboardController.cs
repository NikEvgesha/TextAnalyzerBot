using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TextAnalyzerBot.Configuration;
using TextAnalyzerBot.Services;
using Telegram.Bot.Types.Enums;

namespace TextAnalyzerBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).AnalyzerMode = callbackQuery.Data;

            string analyzeMode = callbackQuery.Data switch
            {
                "length" => " длину сообщения",
                "sum" => " сумму чисел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Бот будет считать {analyzeMode}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Режим можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html); ;
        }

    }
}

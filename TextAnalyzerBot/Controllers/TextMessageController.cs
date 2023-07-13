using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TextAnalyzerBot.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using TextAnalyzerBot.Models;
using TextAnalyzerBot.Services;

namespace TextAnalyzerBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly ITextAnalyzer _textAnalyzer;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, ITextAnalyzer textAnalyzer)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _textAnalyzer = textAnalyzer;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчет длины сообщения" , $"length"),
                        InlineKeyboardButton.WithCallbackData($"Подсчет суммы чисел" , $"sum")
                    });

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Выберите режим работы бота:</b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    string answer = _textAnalyzer.Analyze(message.Text, _memoryStorage.GetSession(message.Chat.Id).AnalyzerMode);

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, answer, cancellationToken: ct);
                    break;

            }
            
            
        }
    }
}

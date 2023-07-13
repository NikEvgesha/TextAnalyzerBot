using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TextAnalyzerBot.Configuration;
using TextAnalyzerBot.Models;

namespace TextAnalyzerBot.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public TextAnalyzer(AppSettings appSettings, ITelegramBotClient telegramBotClient)
        {
            _appSettings = appSettings;
            _telegramBotClient = telegramBotClient;
        }

        public string Analyze(string message, string analyzerMode)
        {
            switch (analyzerMode)
            {
                case "length":
                    try
                    {
                        return "Длина сообщения (без пробелов): " + GetLength(message).ToString() + "\nДлина сообщения (с пробелами): " + message.Length;
                    }
                    catch (Exception ex)
                    {
                        return "Не могу посчитать :(";
                    }
                    
                case "sum":
                    try
                    {
                        return "Сумма чисел: " + GetSum(message).ToString();
                    }
                    catch (Exception ex)
                    {
                        return "Не могу посчитать :(";
                    }
                    
                default: 
                    return "Не могу определить режим :(";
            }
        }
        
        private int GetLength(string message)
        {

                string normalize = message.Replace(" ", "");
                return normalize.Length;


        }

        private int GetSum(string message)
        {

                var nums = message.Split(' ').Select(Int32.Parse).ToArray();
                return nums.Sum();

        }

    }
}

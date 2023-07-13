using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzerBot.Services
{
    public interface ITextAnalyzer
    {

        string Analyze(string message, string analyzerMode);

    }
}

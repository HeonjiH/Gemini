using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini
{
    internal interface IGeminiTest
    {
        Task Build();
        Task RAGTest();
        Task PromptTest();
    }
}

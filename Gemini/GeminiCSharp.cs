using GenerativeAI.Models;
using GenerativeAI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gemini
{
    public class GeminiCSharp
    {
        private readonly string _apiKey;
        public GeminiCSharp(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task Build()
        {
            var model = new GenerativeModel(_apiKey);

            var chat = model.StartChat(new StartChatParams());

            var content = new StringContent("안녕", Encoding.UTF8);
            var message = await content.ReadAsStringAsync();

            var result = await chat.SendMessageAsync(message);
            Console.WriteLine(result);
        }
    }
}

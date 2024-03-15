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
    public class GeminiCSharp : IGeminiTest
    {
        private readonly string _apiKey;
        public GeminiCSharp(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task Build()
        {
            //var model = new GenerativeModel(_apiKey);

            //var handler = new Action<string>((streamMessage) =>
            //{
            //    Console.WriteLine(streamMessage);
            //});

            //var chat = model.StartChat(new StartChatParams());

            //var content = new StringContent("시를 하나 지어줘. 이쁘게", Encoding.UTF8);
            //var message = await content.ReadAsStringAsync();

            //await chat.StreamContentAsync(message, handler);

            //var result = await chat.SendMessageAsync(message);

            await PromptTest();
        }

        public async Task RequestStreamGemini(string message)
        {
            var model = new GenerativeModel(_apiKey);
            var handler = new Action<string>((streamMessage) =>
            {
                Console.WriteLine(streamMessage);
            });

            var chat = model.StartChat(new StartChatParams());
            await chat.StreamContentAsync(message, handler);
        }

        public async Task<string> RequestGemini(string message)
        {
            Console.WriteLine(message.Length);
            var model = new GenerativeModel(_apiKey);
            var chat = model.StartChat(new StartChatParams());
            var result = await chat.SendMessageAsync(message);
            Console.WriteLine(result);

            return result;
        }

        public async Task RAGTest()
        {
            var content = TestTemplate.GetDocument();
            var stringContent = new StringContent(content, Encoding.UTF8);
            var message = await stringContent.ReadAsStringAsync();

            await RequestGemini(message);
        }

        public async Task PromptTest()
        {
            var userInput = "얼마전에 이세계에 갔다왔어. 드래곤도 보고 용왕도 봤어. 특히 인어랑은 짱친이 되었지 뭐야.";
            var actSystemMessage = TestTemplate.ExtractReason(userInput);

            var actResult = await RequestGemini(actSystemMessage);

            var act = actResult.Split("ACT: ")[1].Trim();

            var message = act switch
            {
                "OutOfScope" => TestTemplate.SetupOutOfScopeRequest(act, userInput),
                "Encourage" => TestTemplate.SetupEncourageRequest(act, userInput),
                "Empathy" => TestTemplate.SetupEmpathyRequest(act, userInput),
                "Sorry" => TestTemplate.SetupSorryRequest(act, userInput),
                _ => "",
            };

            await RequestGemini(message);
        }
    }
}

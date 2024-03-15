using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gemini
{
    public class GeminiAPI : IGeminiTest
    {
        private readonly string _apiKey;
        private readonly string _apiUri;
        private readonly HttpClient _httpClient;

        public GeminiAPI(string apiKey, string apiUri, HttpClient httpClient)
        {
            _apiKey = apiKey;
            _apiUri = apiUri;
            _httpClient = httpClient;
        }

        public async Task Build()
        {
            Console.WriteLine("GeminiAPI Build");

            try
            {
                await PromptTest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<string> RequestGemini(string message)
        {
            var jsonBody = $@"{{
                ""contents"": [
                    {{
                        ""parts"": [
                            {{
                                ""text"": ""{message}""
                            }}
                        ]
                    }}
                ]
            }}";

            var uri = $"{_apiUri}?key={_apiKey}";
            var response = await _httpClient.PostAsync(uri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                return result;
            }
            return "";
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

        public async Task RAGTest()
        {
            var content = TestTemplate.GetDocument();
            await RequestGemini(content);
        }
    }
}

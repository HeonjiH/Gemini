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
    public class GeminiAPI
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
                string jsonBody = $@"{{
                ""contents"": [
                    {{
                        ""parts"": [
                            {{
                                ""text"": ""안녕""
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
                }
                else
                {
                    Console.WriteLine("failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

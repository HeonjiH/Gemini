using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini
{
    public class GeminiBuild : IGeminiService
    {
        private readonly string _apiKey;
        private readonly string _apiUri;
        private readonly HttpClient _httpClient;

        public GeminiBuild(IOptions<GeminiOptions> options, HttpClient httpClient)
        {
            var _options = options.Value;
            _apiKey = _options.APIKey;
            _apiUri = _options.APIUri;
            _httpClient = httpClient;
        }

        public async Task Build(RequestTypes types)
        {
            switch (types)
            {
                case RequestTypes.API:
                    var geminiAPI = new GeminiAPI(_apiKey, _apiUri, _httpClient);
                    await geminiAPI.Build();
                    break;
                case RequestTypes.CSharp:
                    var geminiCSharp = new GeminiCSharp(_apiKey);
                    await geminiCSharp.Build();
                    break;
                default:
                    break;
            }
        }

        public Task Build()
        {
            throw new NotImplementedException();
        }
    }
}

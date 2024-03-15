using Gemini;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;


// 구성 빌더 설정
var configuration = new ConfigurationBuilder()
    .SetBasePath("C:\\Users\\dalsa\\OneDrive\\Desktop\\study\\Gemini\\Gemini\\Gemini")
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// 서비스 컬렉션 생성 및 구성
var services = new ServiceCollection();

// 구성을 POCO 설정 클래스에 바인딩
services.Configure<GeminiOptions>(configuration.GetSection("GeminiOptions"));


services.AddHttpClient();
// 종속성 주입 설정
services.AddTransient<IGeminiService, GeminiBuild>();

// 서비스 프로바이더 구성
var serviceProvider = services.BuildServiceProvider();

// 서비스 사용
var myService = serviceProvider.GetService<IGeminiService>();
await myService.Build(RequestTypes.API);
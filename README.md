# Gemini
- RestAPI로 생성하기 (GeminiAPI)
- C#패키지를 사용해서 생성하기 (GeminiCSharp)

## GeminiAPI
1. Program.cs
   ``` C#
   ...
   await myService.Build(RequestTypes.API);
   ```
2. GeminiAPI - in Build Method
   ``` C#
   /*Prompt Test*/
   await PromptTest();
   /*Rag Test*/
   await RAGTest();
   ```
## GeminiCSharp
1. Program.cs
   ``` C#
   ...
   await myService.Build(RequestTypes.CSharp);
   ```
2. GeminiCSharp - in Build Method
   ``` C#
   /*Prompt Test*/
   await PromptTest();
   /*Rag Test*/
   await RAGTest();
   ```

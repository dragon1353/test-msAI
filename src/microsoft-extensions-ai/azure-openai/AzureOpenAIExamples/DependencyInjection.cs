﻿using OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.AI.OpenAI;
using Azure.Identity;

public partial class OpenAISamples
{
    public static async Task DependencyInjection() 
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddSingleton(
            new AzureOpenAIClient(
                new Uri(Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")),
                new DefaultAzureCredential()));
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddChatClient(services => services.GetRequiredService<OpenAIClient>().AsChatClient("gpt-4o-mini"))
            .UseDistributedCache();

        var app = builder.Build();

        var chatClient = app.Services.GetRequiredService<IChatClient>();

        Console.WriteLine(await chatClient.GetResponseAsync("What is AI?"));
    }        
}

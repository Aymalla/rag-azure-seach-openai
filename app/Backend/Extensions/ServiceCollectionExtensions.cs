// Copyright (c) Microsoft. All rights reserved.

using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Search.Documents;
using Azure.Storage.Blobs;

namespace backend.Extensions;

internal static class ServiceCollectionExtensions
{
    private static readonly DefaultAzureCredential s_azureCredential = new();

    internal static IServiceCollection AddAzureServices(this IServiceCollection services)
    {
        services.AddSingleton<BlobServiceClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var azureStorageAccountEndpoint = config["AzureStorageAccountEndpoint"];
            ArgumentNullException.ThrowIfNullOrEmpty(azureStorageAccountEndpoint);

            return new BlobServiceClient(new Uri(azureStorageAccountEndpoint), s_azureCredential);
        });

        services.AddSingleton<BlobContainerClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var azureStorageContainer = config["AzureStorageContainer"];
            return sp.GetRequiredService<BlobServiceClient>().GetBlobContainerClient(azureStorageContainer);
        });

        services.AddSingleton<SearchClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var (azureSearchServiceEndpoint, azureSearchIndex) = (config["AzureSearchServiceEndpoint"], config["AzureSearchIndex"]);
            ArgumentNullException.ThrowIfNullOrEmpty(azureSearchServiceEndpoint);

            return new SearchClient(new Uri(azureSearchServiceEndpoint), azureSearchIndex, s_azureCredential);
        });

        services.AddSingleton<DocumentAnalysisClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var azureOpenAiServiceEndpoint = config["AzureOpenAiServiceEndpoint"] ?? throw new ArgumentNullException();

            return new DocumentAnalysisClient(new Uri(azureOpenAiServiceEndpoint), s_azureCredential);
        });

        services.AddSingleton<OpenAIClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var azureOpenAiServiceEndpoint = config["AzureOpenAiServiceEndpoint"];
            ArgumentNullException.ThrowIfNullOrEmpty(azureOpenAiServiceEndpoint);

            return new OpenAIClient(new Uri(azureOpenAiServiceEndpoint), s_azureCredential);
        });

        //services.AddSingleton<AzureBlobStorageService>();
        //services.AddSingleton<ReadRetrieveReadChatService>();

        return services;
    }

    internal static IServiceCollection AddCrossOriginResourceSharing(this IServiceCollection services)
    {
        services.AddCors(
            options =>
                options.AddDefaultPolicy(
                    policy =>
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()));

        return services;
    }
}

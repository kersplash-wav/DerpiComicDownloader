using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Services;

public static class NetworkService
{
    private static readonly HttpClient Client = new();

    public static async Task<string> DownloadFileAsync(string downloadLink, string fileName)
    {
        // Create Path //
        string filePath = $"{PanelsTemp}/{fileName}";
        // Fetch //
        using Stream response = await Client.GetStreamAsync(downloadLink);
        // Download //
        using FileStream output = new(filePath, FileMode.Create);
        // Install //
        await response.CopyToAsync(output);
        // Return Path //
        return filePath;
    }
}

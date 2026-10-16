using Core.Services;
using static Core.Services.GlobalService;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DerpiComicDownloader;

class Program
{
    const string Domain = "https://derpibooru.org";
    static readonly HttpClient Client = new();
    #region Structs
    #pragma warning disable IDE1006 // Naming Styles
    struct Representations
    {
        public string full { get; set; }
        public string small {get; set; }
        public string thumb_tiny {get; set; }
        public string thumb_small {get; set; }
        public string thumb {get; set; }
        public string medium {get; set; }
        public string large {get; set; }
        public string tall {get; set; }
    }
    struct Image
    {
        public string name { get; set; }
        public string created_at { get; set; }
        public string description { get; set; }
        public int? duplicate_of { get; set; }
        public int orig_size { get; set; }
        public string mime_type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string view_url { get; set; }
        public string[] source_urls { get; set; }
        public int id { get; set; }
        public Representations representations { get; set; }
        public string format { get; set; }
        public int? uploader_id;
        public string? deletion_reason;
    }
    struct TagResult
    {
        string name { get; set; }
    }

    struct SearchResult
    {
        public int total { get; set; }
        public Image[] images { get; set; }
    }
    #pragma warning restore IDE1006 // Naming Styles
    #endregion
    #region Base Methods
    private static async Task<bool> VerifyTag(string tag)
    {
        string query = $"{Domain}/api/v1/json/tags/{tag}";
        // Debug //
        Console.WriteLine($"Verifying: '{query}'");
        // Request Data //
        try
        {
            string data = await Client.GetStringAsync(query);
            // Deserialize //
            TagResult result = JsonSerializer.Deserialize<TagResult>(data);
            // Success //
            return true;
        }

        catch (HttpRequestException)
        {
            // Debug //
            Console.WriteLine($"The tag '{tag}' does not exist.");
        }
        // Fail //
        return false;
    }
    private static async Task<SearchResult> Search(params string[] tags)
    {
        string query = $"{Domain}/api/v1/json/search/images?q={string.Join(',', tags)}";
        // Debug //
        Console.WriteLine($"Searching: '{query}'");
        // Request Data //
        string data = await Client.GetStringAsync(query);
        // Deserialize //
        SearchResult result = JsonSerializer.Deserialize<SearchResult>(data);
        Console.WriteLine(result);
        // Give //
        return result;
    }
    private static async Task<string> DownloadPage(string sourceURL, int panelIndex)
    {
        // If you want it as PNG
        string fileName = string.Format("-{0,8:D8}", panelIndex);
        string downloadedTo = @$"{PanelsTemp}\{fileName}.png";
        // Attempt Download //
        return await NetworkService.DownloadFileAsync(sourceURL, downloadedTo);
    }
    private static byte Prompt()
    {
        Console.WriteLine(
            "1. Tag Verifier" +
            "2. Downloader" +
            "2. Editor");

        return InputService.RequestByte("Select an Option", 1, 3);
    }
    public static async Task Main()
    {
        byte option = Prompt();

        switch (option)
        {
            case 1:

                break;
            case 2:
                break;
            case 3:
                throw new NotImplementedException();
            default:
                Console.WriteLine("wtf?");
                break;
        }

        while (true)
        {
            string comictag = InputService.RequestString("What is the comic tag? (example—comic:unlucky day)");
            string artisttag = InputService.RequestString("What is the artist tag? (example—artist:piesinful");
            // Verify Tags //
            if (!await VerifyTag(comictag) || !await VerifyTag(artisttag))
                continue;
            // Search //
            SearchResult result = await Search(comictag, artisttag);
            // Debug //
            Console.WriteLine($"{result.total} results!");
            // Images //
            foreach (Image page in result.images)
            {
                
            }
        }
    }
    #endregion
}
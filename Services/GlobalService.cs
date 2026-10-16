global using static Core.Services.GlobalService;

namespace Core.Services;

public static class GlobalService
{
    // Constants //
    public const string baseURL = "https://www.canterlotcomics.com"; // Canterlot Comics Website //
    public const string ProgramName = "DerpiComicDownloader";
    public static readonly string ProgramFiles = @$"{Environment.ExpandEnvironmentVariables("%ProgramW6432%")}\{ProgramName}";
    public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public static readonly string AppData = $@"{LocalAppData}\{ProgramName}";
    public static readonly string PanelsTemp = $@"{AppData}\PanelsTemp";
}

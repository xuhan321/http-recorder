using HarmonyLib;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace HttpRecorder;

public class MainPlugin : Plugin<Config>
{
    public static MainPlugin? Instance { get; private set; }
    public override string Name => "HttpRecorder";
    public override string Description => "记录常见 HTTP 请求（HttpClient、HttpWebRequest、WebClient），用于后门排查。";
    public override string Author => "xuhan321";
    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;
    private Harmony? _harmony;
    public override void Enable()
    {
        Instance = this;
        if (_harmony == null)
        {
            _harmony = new Harmony("xh321.http-recorder");
            _harmony.PatchAll();
        }
    }
    public override void Disable()
    {
        _harmony?.UnpatchAll();
        _harmony = null;
        Instance = null;
    }
}
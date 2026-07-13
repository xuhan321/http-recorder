using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.DownloadString), typeof(string))]
internal static class WebClientDownloadStringPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [GET] 同步 下载字符串: {address}", address);
    }
}
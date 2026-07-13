using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.DownloadStringTaskAsync), typeof(string))]
internal static class WebClientDownloadStringTaskAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [GET] 异步 下载字符串: {address}", address);
    }
}
using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.DownloadDataTaskAsync), typeof(string))]
internal static class WebClientDownloadDataTaskAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [GET] 异步 下载数据: {address}", address);
    }
}

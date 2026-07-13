using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.DownloadData), typeof(string))]
internal static class WebClientDownloadDataPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [GET] 同步 下载数据: {address}", address);
    }
}
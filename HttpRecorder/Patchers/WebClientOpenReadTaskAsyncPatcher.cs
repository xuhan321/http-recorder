using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.OpenReadTaskAsync), typeof(string))]
internal static class WebClientOpenReadTaskAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [GET] 异步 打开读取流: {address}", address);
    }
}
using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(HttpWebRequest), nameof(HttpWebRequest.GetRequestStreamAsync), [])]
internal static class HttpWebRequestGetRequestStreamAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(HttpWebRequest __instance, ref Task<Stream> __result)
    {
        var url = __instance.RequestUri.ToString();
        LoggerHelper.Write($"[HttpWebRequest] [{__instance.Method}] 异步获取请求流 -> {url}", url);
    }
}
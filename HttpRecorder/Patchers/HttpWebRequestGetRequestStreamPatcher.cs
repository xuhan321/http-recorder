using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(HttpWebRequest), nameof(HttpWebRequest.GetRequestStream), [])]
internal static class HttpWebRequestGetRequestStreamPatcher
{
    [HarmonyPostfix]
    public static void Postfix(HttpWebRequest __instance)
    {
        var url = __instance.RequestUri.ToString();
        LoggerHelper.Write($"[HttpWebRequest] [{__instance.Method}] 同步获取请求流 -> {url}", url);
    }
}
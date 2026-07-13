using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.OpenWrite), typeof(string))]
internal static class WebClientOpenWritePatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [POST/PUT] 同步 打开写入流: {address}", address);
    }
}
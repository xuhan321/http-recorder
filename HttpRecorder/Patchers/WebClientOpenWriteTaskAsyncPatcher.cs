using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.OpenWriteTaskAsync), typeof(string))]
internal static class WebClientOpenWriteTaskAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address)
    {
        LoggerHelper.Write($"[WebClient] [POST/PUT] 异步 打开写入流: {address}", address);
    }
}
using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.UploadString), typeof(string), typeof(string))]
internal static class WebClientUploadStringPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address, string data)
    {
        LoggerHelper.Write($"[WebClient] [POST/PUT] 同步 上传字符串: {address} (数据大小: {data?.Length ?? 0})", address);
    }
}
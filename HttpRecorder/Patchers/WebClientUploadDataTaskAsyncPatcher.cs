using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.UploadDataTaskAsync), typeof(string), typeof(byte[]))]
internal static class WebClientUploadDataTaskAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address, byte[] data)
    {
        LoggerHelper.Write($"[WebClient] [POST/PUT] 异步 上传数据: {address} (数据大小: {data?.Length ?? 0} 字节)", address);
    }
}
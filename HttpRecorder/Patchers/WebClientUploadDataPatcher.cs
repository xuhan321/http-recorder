using System.Net;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(WebClient), nameof(WebClient.UploadData), typeof(string), typeof(byte[]))]
internal static class WebClientUploadDataPatcher
{
    [HarmonyPostfix]
    public static void Postfix(string address, byte[] data)
    {
        LoggerHelper.Write($"[WebClient] [POST/PUT] 同步 上传数据: {address} (数据大小: {data?.Length ?? 0} 字节)", address);
    }
}
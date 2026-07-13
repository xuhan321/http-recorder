using System.Net.Http;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(HttpClient), nameof(HttpClient.SendAsync), typeof(HttpRequestMessage), typeof(HttpCompletionOption), typeof(CancellationToken))]
internal static class HttpClientSendAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(HttpRequestMessage request)
    {
        var url = request.RequestUri.ToString();
        LoggerHelper.Write($"[HttpClient] [{request.Method}] 异步交互Url: {url}", url);
    }
}
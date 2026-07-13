
using System.Net.WebSockets;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(ClientWebSocket), nameof(ClientWebSocket.ConnectAsync), typeof(Uri), typeof(CancellationToken))]
internal static class ClientWebSocketConnectAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(ClientWebSocket __instance, Uri uri)
    {
        var url = uri.ToString();
        WebSocketTracker.SetOwnerUrl(__instance, url);
        LoggerHelper.Write($"[WebSocket] 连接: {uri}", url);
    }
}
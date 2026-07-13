using System.Net.WebSockets;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(ClientWebSocket), nameof(ClientWebSocket.ReceiveAsync),
    typeof(ArraySegment<byte>),
    typeof(CancellationToken))]
internal static class ClientWebSocketReceiveAsyncPatcher
{
    [HarmonyPostfix]
    public static void Postfix(ClientWebSocket __instance, ArraySegment<byte> buffer)
    {
        var url = WebSocketTracker.GetOwnerUrl(__instance);
        LoggerHelper.Write($"[WebSocket] 从 {url} 接收数据 (大小: {buffer.Count} 字节)", url);
    }
}
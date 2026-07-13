using System.Net.WebSockets;
using HarmonyLib;

namespace HttpRecorder.Patchers;

[HarmonyPatch(typeof(ClientWebSocket), nameof(ClientWebSocket.SendAsync),
    typeof(ArraySegment<byte>),
    typeof(WebSocketMessageType),
    typeof(bool),
    typeof(CancellationToken))]
internal static class ClientWebSocketSendAsyncPatcher
{
    [HarmonyPostfix]
#pragma warning disable IDE0060 // 删除未使用的参数
    public static void Postfix(ClientWebSocket __instance, ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage)
#pragma warning restore IDE0060 // 删除未使用的参数
    {
        var url = WebSocketTracker.GetOwnerUrl(__instance);
        LoggerHelper.Write($"[WebSocket] 发送数据到 {url} (类型: {messageType}, 大小: {buffer.Count} 字节)", url);
    }
}
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace HttpRecorder;

internal static class WebSocketTracker
{
    private static readonly ConditionalWeakTable<ClientWebSocket, string> _urlMap = new();
    public static void SetOwnerUrl(ClientWebSocket ws, string url)
    {
        if (!_urlMap.TryGetValue(ws, out _))
        {
            _urlMap.Add(ws, url);
        }
    }
    public static string GetOwnerUrl(ClientWebSocket ws) => _urlMap.TryGetValue(ws, out var url) ? url : "未知地址";
}

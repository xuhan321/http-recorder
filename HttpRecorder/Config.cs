using System.ComponentModel;

namespace HttpRecorder;

public class Config
{
    [Description("是否记录调用堆栈（用于定位是哪个插件发起的请求）。启用后可能略微影响性能。")]
    public bool RecordCallStack { get; set; } = false;

    [Description("记录调用堆栈栈追踪深度（获取调用栈层总数），0表示全部。")]
    public int StackTraceDepth { get; set; } = 0;
    [Description("要忽略的URL列表（包含此字符串的URL将不会记录日志）。")]
    public List<string> IgnoredUrls { get; set; } =
    [
        "sbg1.scpslgame.com",
        "api.scpslgame.com",
        "scpslgame.com"
    ];
}

using System.Diagnostics;
using LabApi.Features.Console;
using NorthwoodLib.Pools;

namespace HttpRecorder;

internal static class LoggerHelper
{
    private const int SkipFrames = 2;

    public static void Write(string message, string? url = null)
    {
        var config = MainPlugin.Instance?.Config;
        if (config == null)
            return;

        if (!string.IsNullOrEmpty(url) && config.IgnoredUrls != null)
        {
            foreach (var ignored in config.IgnoredUrls)
            {
                if (url?.IndexOf(ignored, StringComparison.OrdinalIgnoreCase) >= 0)
                    return;
            }
        }

        if (!config.RecordCallStack)
        {
            Logger.Info(message);
            return;
        }

        var frames = new StackTrace(SkipFrames, false).GetFrames();
        if (frames == null || frames.Length == 0)
        {
            Logger.Info($"{message} [调用者: 无法获取]");
            return;
        }

        int depth = config.StackTraceDepth <= 0 ? frames.Length : Math.Min(config.StackTraceDepth, frames.Length);
        var sb = StringBuilderPool.Shared.Rent();
        sb.Append(message).Append(" [调用者: ");

        for (int i = 0; i < depth; i++)
        {
            var method = frames[i].GetMethod();
            if (method is null) continue;

            var type = method.DeclaringType;
            sb.Append(type is not null ? $"{type.FullName}.{method.Name}" : method.Name);
            if (i < depth - 1) sb.Append(" -> ");
        }

        sb.Append(']');
        Logger.Info(StringBuilderPool.Shared.ToStringReturn(sb));
    }
}
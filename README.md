# HttpRecorder

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**HttpRecorder** 是一个简单的、专门用于排查简易后门的插件，依赖于**LabAPI**和**HarmonyLib**。

通过HarmonyPostfix补丁捕获常见的 HTTP/HTTPS 请求（包括WebSocket握手），并在控制台输出请求日志，支持调用堆栈追踪与 URL 过滤，帮助服主快速发现可疑插件。

---

## ✨ 功能特点

- **捕获大**：覆盖 `HttpClient`、`HttpWebRequest`、`WebClient`、`ClientWebSocket` 的常用同步/异步方法。
- **捕获全**：直接输出发起请求的代码调用链（精确到方法名），快速定位可疑插件。
- **捕获准**：支持通过配置文件忽略指定链接，默认已屏蔽SCP:SL中心服务器地址，减少干扰。

---

## ⚙️ 配置说明

配置文件 `config.yml` 包含以下选项：

| 参数 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| `RecordCallStack` | bool | `false` | 是否记录调用堆栈（用于定位发起请求的插件）。 |
| `StackTraceDepth` | int | `0` | 调用堆栈输出层数，`0` 表示输出全部。 |
| `IgnoredUrls` | List<string> | `["sbg1.scpslgame.com", "api.scpslgame.com", "scpslgame.com"]` | 要忽略的 URL 片段列表（包含即忽略，不区分大小写）。 |

---

## 📝 日志示例

```text
[HttpClient] [GET] 异步交互Url: https://XXXXXXXXX/admin_list [调用者: SomePlugin.HttpClientHelper.FetchData -> System.Net.Http.HttpClient.SendAsync]
// 假设：请求后门管理列表，通过调用链定位到插件 "SomePlugin"

[WebSocket] 发送数据到 wss://XXXXXXXXX/websocket_service (类型: Text, 大小: 128 字节) [调用者: BackdoorPlugin.CommandHandlers.SendCommand]
// 假设：连接远程 WebSocket 后门命令通道，通过调用链定位到插件 "BackdoorPlugin"
```

---

> ⚠️ **注意**：本插件仅用于辅助排查，不包含自动威胁识别能力，最终判断需服主结合源代码进行人工分析。

当然，服主要做好对后门的防范，警惕第三方的 HTTP 库、不知名的 File IO 操作、被打包的未知依赖和 Win32API 等可疑的调用。

推荐使用dnSpyEx对疑似后门的插件进行反编译：
[【dnSpyEx 仓库链接】](https://github.com/dnSpyEx/dnSpy)

> 同时，一定要记住道理： **永远不要盲目相信任何人，即使是自己服务器的技术！**

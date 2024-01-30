
namespace Frontend.Utils;

public sealed partial class JavaScriptModule
{
    [JSImport("listenForIFrameLoaded", nameof(JavaScriptModule))]
    public static partial Task RegisterIFrameLoadedAsync(string selector, [JSMarshalAs<JSType.Function>] Action onLoaded);
}

using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using Serilog;

namespace AvaloniaProofOfConcept
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);

#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }
    }
}

using Avalonia;
using Avalonia.Logging.Serilog;
using AvaloniaProofOfConcept.ViewModels;
using AvaloniaProofOfConcept.Views;

namespace AvaloniaProofOfConcept
{
    public class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}

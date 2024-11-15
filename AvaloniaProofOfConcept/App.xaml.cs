using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Simple;
using AvaloniaProofOfConcept.ViewModels;
using AvaloniaProofOfConcept.Views;

namespace AvaloniaProofOfConcept;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

        var theme = new SimpleTheme();
        theme.TryGetResource("Button", ThemeVariant.Dark, out _);

        base.OnFrameworkInitializationCompleted();
    }
}
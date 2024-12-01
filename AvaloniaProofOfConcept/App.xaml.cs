using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Simple;
using AvaloniaProofOfConcept.ViewModels;
using AvaloniaProofOfConcept.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Concurrency;

namespace AvaloniaProofOfConcept;

public sealed class App : Application
{
    private readonly ServiceProvider _services;

    public App()
    {
        _services = new ServiceCollection()
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<IMessenger, StrongReferenceMessenger>()
            .AddSingleton<ProcessesViewModel>()
            .AddSingleton<IScheduler, TaskPoolScheduler>(provider => TaskPoolScheduler.Default)
            .BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var viewModel = _services.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            await viewModel.Initialize();

            desktop.ShutdownRequested += (_, _) =>
            {
                _services.Dispose();
            };
        }

        var theme = new SimpleTheme();
        theme.TryGetResource("Button", ThemeVariant.Dark, out _);

        base.OnFrameworkInitializationCompleted();
    }
}
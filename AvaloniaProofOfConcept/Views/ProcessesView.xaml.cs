using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaProofOfConcept.Views;

// ReSharper disable once UnusedType.Global
public sealed class ProcessesView : UserControl
{
    public ProcessesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
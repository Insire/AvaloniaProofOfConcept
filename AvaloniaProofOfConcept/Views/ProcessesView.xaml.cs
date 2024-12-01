using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaProofOfConcept.Views;

public class ProcessesView : UserControl
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
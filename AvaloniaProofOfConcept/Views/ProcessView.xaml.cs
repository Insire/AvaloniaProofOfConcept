using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaProofOfConcept.Views;

public class ProcessView : UserControl
{
    public ProcessView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
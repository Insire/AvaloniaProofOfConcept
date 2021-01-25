using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaProofOfConcept
{
    public class ProcessView : UserControl
    {
        public ProcessView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

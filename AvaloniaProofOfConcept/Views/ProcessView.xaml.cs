using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaProofOfConcept.ViewModels;

namespace AvaloniaProofOfConcept.Views
{
    public class ProcessView : UserControl
    {
        public ProcessView()
        {
            DataContext = new ProcessViewModel();

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

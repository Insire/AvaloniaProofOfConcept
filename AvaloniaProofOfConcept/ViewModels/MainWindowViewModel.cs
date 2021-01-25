using System.Collections.ObjectModel;

namespace AvaloniaProofOfConcept
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ViewModelBase> Items { get; }

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<ViewModelBase>
            {
                new ProcessViewModel()
            };
        }
    }
}

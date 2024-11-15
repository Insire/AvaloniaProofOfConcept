using System.Collections.ObjectModel;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Process = new ProcessViewModel();
        Items = [Process];
    }

    public ProcessViewModel Process { get; }
    public ObservableCollection<ViewModelBase> Items { get; }
}
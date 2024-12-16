using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(ProcessesViewModel processesViewModel)
    {
        Process = processesViewModel;
        
        var items = new ObservableCollection<ObservableObject>
        {
            Process
        };
        Items = new ReadOnlyObservableCollection<ObservableObject>(items);
    }

    public ProcessesViewModel Process { get; }

    public ReadOnlyObservableCollection<ObservableObject> Items { get; }

    public Task Initialize()
    {
        return Process.SearchCommand.ExecuteAsync("");
    }
}
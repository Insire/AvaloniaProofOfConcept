using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject
{
    private readonly ObservableCollection<ObservableObject> _items;

    public MainWindowViewModel(ProcessesViewModel processesViewModel)
    {
        _items = new ObservableCollection<ObservableObject>();
        Items = new ReadOnlyObservableCollection<ObservableObject>(_items);

        Process = processesViewModel;
        _items.Add(Process);
    }

    public ProcessesViewModel Process { get; }

    public ReadOnlyObservableCollection<ObservableObject> Items { get; }

    public async Task Initialize()
    {
        await Process.SearchCommand.ExecuteAsync("");
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed class ProcessViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<IEnumerable<Process>> _searchResults;
    private readonly string _searchTerm;

    public ProcessViewModel()
    {
        SearchTerm = "svchost";
        ExecuteSearch =
            ReactiveCommand.CreateFromTask<string, IEnumerable<Process>>(searchTerm => GetProcesses(searchTerm));

        this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
            .Select(x => x?.Trim())
            .DistinctUntilChanged()
            .InvokeCommand(ExecuteSearch);

        _searchResults = ExecuteSearch.ToProperty(this, x => x.SearchResults, new List<Process>());
    }

    private string SearchTerm
    {
        get => _searchTerm;
        init => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }

    private ReactiveCommand<string, IEnumerable<Process>> ExecuteSearch { get; }

    public IEnumerable<Process> SearchResults => _searchResults.Value;

    private static async Task<IEnumerable<Process>> GetProcesses(string processName)
    {
        var processes = await Task.Run(() => Process.GetProcesses(Environment.MachineName));

        if (string.IsNullOrEmpty(processName))
            return processes;

        var result = processes
            .Where(p =>
                p.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase) 
                || p.ProcessName.Contains(processName))
            .OrderBy(p=>p.ProcessName)
            .ThenBy(p=> p.Id)
            .ToList();

        return result;
    }
}
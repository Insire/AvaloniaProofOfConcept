using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed partial class ProcessesViewModel : ObservableRecipient, IDisposable
{
    private readonly CompositeDisposable _subscription;
    private readonly ObservableCollectionExtended<ProcessViewModel> _searchResults;
    private readonly SourceCache<ProcessViewModel, int> _cache;

    [ObservableProperty]
    private string? _searchTerm;

    public ReadOnlyObservableCollection<ProcessViewModel> SearchResults { get; }

    public ProcessesViewModel(IMessenger messenger, IScheduler scheduler)
        : base(messenger)
    {
        _searchTerm = string.Empty;
        _searchResults = new ObservableCollectionExtended<ProcessViewModel>();
        _cache = new SourceCache<ProcessViewModel, int>(p => p.Id);

        var cacheSubscription = _cache
            .Connect()
            .ObserveOn(scheduler)
            .DistinctUntilChanged()
            .AutoRefresh()
            .ObserveOn(scheduler)
            .SortAndBind(_searchResults, SortExpressionComparer<ProcessViewModel>
                .Ascending(p => p.ProcessName)
                .ThenByAscending(p => p.Id))
            .Subscribe();

        var messengerSubscription = this.WhenPropertyChanged(x => x.SearchTerm)
            .DistinctUntilChanged()
            .Throttle(TimeSpan.FromMilliseconds(250), scheduler)
            .Subscribe(p => SearchCommand.ExecuteAsync(p.Value?.Trim()));

        _subscription = new CompositeDisposable(cacheSubscription, messengerSubscription);

        SearchResults = new ReadOnlyObservableCollection<ProcessViewModel>(_searchResults);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task Search(object? argument)
    {
        if (argument is string searchTerm)
        {
            var results = await GetProcesses(searchTerm);

            _cache.Edit(source =>
            {
                var entriesToDelete = source.Keys.ToHashSet();
                foreach (var result in results)
                {
                    var key = source.GetKey(result);

                    var optionalViewModel = source.Lookup(key);
                    if (optionalViewModel.HasValue)
                    {
                        optionalViewModel.Value.MachineName = result.MachineName;
                        optionalViewModel.Value.ProcessName = result.ProcessName;
                    }
                    else
                    {
                        source.AddOrUpdate(result);
                    }

                    entriesToDelete.Remove(key);
                }

                source.RemoveKeys(entriesToDelete);
            });

            _cache.AddOrUpdate(results);
        }
        else
        {
            var results = await GetProcesses(SearchTerm?.Trim());

            _cache.AddOrUpdate(results);
        }
    }

    private static async Task<IReadOnlyList<ProcessViewModel>> GetProcesses(string? processName)
    {
        var processes = await Task.Run(() => 
            Process.GetProcesses()
            .Where(p => p.Id > 0 && string.IsNullOrEmpty(p.ProcessName) == false)
            .ToArray());

        if (string.IsNullOrEmpty(processName))
        {
            return processes.Select(Map).ToArray();
        }

        var result = processes
            .Where(p =>
                p.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase)
                || p.ProcessName.Contains(processName))
            .Select(Map)
            .ToArray();

        return result;
    }

    private static ProcessViewModel Map(Process process)
    {
        using (process)
        {
            return new ProcessViewModel
            {
                Id = process.Id,
                ProcessName = process.ProcessName,
                MachineName = process.MachineName,
                SessionId = process.SessionId,
                Threads = process.Threads.Count,
                VirtualMemorySizeX64 = process.VirtualMemorySize64,
                PeakVirtualMemorySize64 = process.PeakVirtualMemorySize64,
                PagedMemorySize64 = process.PagedMemorySize64,
                PagedSystemMemorySize64 = process.PagedSystemMemorySize64,
                PeakPageMemorySize64 = process.PeakPagedMemorySize64,
                PeakWorkingSetX64 = process.PeakWorkingSet64,
                PrivateMemorySizeX64 = process.PrivateMemorySize64,
            };
        }
    }

    public void Dispose()
    {
        _subscription.Dispose();
        _cache.Dispose();
    }
}
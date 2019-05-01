using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaProofOfConcept.ViewModels
{
    public sealed class ProcessViewModel : ViewModelBase
    {
        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { this.RaiseAndSetIfChanged(ref _searchTerm, value); }
        }

        public ReactiveCommand<string, IEnumerable<Process>> ExecuteSearch { get; private set; }

        private readonly ObservableAsPropertyHelper<bool> _spinnerVisibility;
        public bool SpinnerVisibility => _spinnerVisibility.Value;

        private ObservableAsPropertyHelper<IEnumerable<Process>> _searchResults;
        public IEnumerable<Process> SearchResults => _searchResults.Value;

        public ProcessViewModel()
        {
            SearchTerm = "svchost";
            ExecuteSearch = ReactiveCommand.CreateFromTask<string, IEnumerable<Process>>(searchTerm => GetProcesses(searchTerm));

            this.WhenAnyValue(x => x.SearchTerm)
                        .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                        .Select(x => x?.Trim())
                                                .DistinctUntilChanged()
                        .InvokeCommand(ExecuteSearch);

            _spinnerVisibility = ExecuteSearch.IsExecuting.ToProperty(this, x => x.SpinnerVisibility, false);

            ExecuteSearch.ThrownExceptions.Subscribe(ex => {/* Handle errors here */});

            _searchResults = ExecuteSearch.ToProperty(this, x => x.SearchResults, new List<Process>());
        }

        public static async Task<IEnumerable<Process>> GetProcesses(string processName)
        {
            var processes = await Task.Run(() => Process.GetProcesses(Environment.MachineName));

            if (string.IsNullOrEmpty(processName))
                return processes;

            var result = processes.Where(p => p.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase) || p.ProcessName.Contains(processName)).ToList();

            return result;
        }
    }
}

using AvaloniaProofOfConcept.Views;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaProofOfConcept.ViewModels
{
    public sealed class NavigationViewModel : ViewModelBase
    {
        public List<ViewViewModel> Views { get; }

        private ViewViewModel _selectedView;
        public ViewViewModel SelectedView
        {
            get { return _selectedView; }
            set { this.RaiseAndSetIfChanged(ref _selectedView, value); }
        }

        public NavigationViewModel()
        {
            Views = new List<ViewViewModel>()
            {
                new ViewViewModel()
                {
                    Control = new ProcessView(),
                    Header ="Processes"
                },
            };

            SelectedView = Views.First();
        }
    }
}

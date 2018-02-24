namespace AvaloniaProofOfConcept.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        public NavigationViewModel Navigation { get; }

        public MainWindowViewModel()
        {
            Navigation = new NavigationViewModel();
        }
    }
}

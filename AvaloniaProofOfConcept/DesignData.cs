using AvaloniaProofOfConcept.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Reactive.Concurrency;

namespace AvaloniaProofOfConcept;

public static class DesignData
{
    public static ProcessesViewModel ProcessViewModel { get; } = new ProcessesViewModel(new WeakReferenceMessenger(), TaskPoolScheduler.Default)
    {
        // View Model initialization here.
    };
}
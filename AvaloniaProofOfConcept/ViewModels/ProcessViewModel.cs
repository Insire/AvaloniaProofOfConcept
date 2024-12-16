using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaProofOfConcept.ViewModels;

public sealed partial class ProcessViewModel : ObservableObject
{
    [ObservableProperty]
    private string _processName = string.Empty;

    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _threads;

    [ObservableProperty]
    private int _sessionId;

    [ObservableProperty]
    private string? _machineName;

    [ObservableProperty]
    private long _pagedMemorySize64;

    [ObservableProperty]
    private long _pagedSystemMemorySize64;

    [ObservableProperty]
    private long _peakPageMemorySize64;

    [ObservableProperty]
    private long _peakVirtualMemorySize64;

    [ObservableProperty]
    private long _virtualMemorySizeX64;

    [ObservableProperty]
    private long _peakWorkingSetX64;

    [ObservableProperty]
    private long _privateMemorySizeX64;
}
using ReactiveUI;
using System;
using System.Reactive;

namespace attestationApp.ViewModels;

public class MainViewModel : ReactiveObject, IScreen
{
    public MainViewModel()
    {
    }
    public RoutingState Router { get; set; } = new RoutingState();

}

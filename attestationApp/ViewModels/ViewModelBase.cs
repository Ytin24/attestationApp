using ReactiveUI;
using System;

namespace attestationApp.ViewModels;

public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public ViewModelBase(IScreen screen)
    {
        HostScreen = screen;
    }
    public string? UrlPathSegment => Guid.NewGuid().ToString();

    public IScreen HostScreen { get; set; }
}

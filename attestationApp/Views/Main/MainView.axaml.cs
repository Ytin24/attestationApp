using attestationApp.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace attestationApp.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
}

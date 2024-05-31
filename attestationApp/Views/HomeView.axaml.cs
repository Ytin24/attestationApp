using attestationApp.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace attestationApp.Views
{
    public partial class HomeView : ReactiveUserControl<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}

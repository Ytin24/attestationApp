using attestationApp.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace attestationApp.Views
{
    public partial class TestView : ReactiveUserControl<TestViewModel>
    {
        public TestView()
        {
            InitializeComponent();
        }
    }
}

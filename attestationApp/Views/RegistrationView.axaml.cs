using Avalonia.Controls;
using Avalonia.ReactiveUI;
using attestationApp.ViewModels;

namespace VolunteerHub.Views {
    public partial class RegistrationView : ReactiveUserControl<RegistrationViewModel> {
        public RegistrationView() {
            InitializeComponent();
        }
    }
}

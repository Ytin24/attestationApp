using Avalonia.ReactiveUI;
using attestationApp.ViewModels;

namespace attestationApp.Views;

public partial class AuthView : ReactiveUserControl<AuthViewModel> {
    public AuthView() {
        InitializeComponent();
    }
}

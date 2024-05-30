
using Avalonia.Threading;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace attestationApp.ViewModels;

public class AuthViewModel : ViewModelBase {

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Unit> LoginAccount { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToRegisterAccount { get; set; }

    public AuthViewModel(IScreen screen, RegistrationViewModel registrationViewModel) : base(screen)
    {

        GoToRegisterAccount = 
            ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(registrationViewModel)
            );
    }
    
}

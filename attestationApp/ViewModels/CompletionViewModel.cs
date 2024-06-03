using attestationApp.DB;
using attestationApp.Services;
using ReactiveUI;
using System.Linq;
using System.Reactive;

namespace attestationApp.ViewModels
{
    public class CompletionViewModel : ViewModelBase
    {
        public CompletionViewModel(IScreen screen) : base(screen)
        {
            RestartCommand = ReactiveCommand.Create(RestartApplication);

        }
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }
        public ReactiveCommand<Unit, Unit> RestartCommand { get; }

        private void RestartApplication()
        {
            HostScreen.Router.NavigateAndReset.Execute(HostScreen.Router.NavigationStack.First());
        }
    }
}

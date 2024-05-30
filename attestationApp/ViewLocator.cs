using attestationApp.ViewModels;
using Avalonia.Controls.Templates;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using attestationApp.Views;
using ReactiveUI;
using VolunteerHub.Views;

namespace attestationApp
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch
        {
            MainViewModel ctx => new MainView() { DataContext= ctx },
            AuthViewModel ctx => new AuthView() { DataContext = ctx },
            RegistrationViewModel ctx => new RegistrationView() { DataContext = ctx },
            _ => throw new ArgumentNullException(nameof(viewModel))
        };
    }
}

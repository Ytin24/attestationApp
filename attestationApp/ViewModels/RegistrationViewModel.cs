using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace attestationApp.ViewModels {
    public class RegistrationViewModel : ViewModelBase {
        public ReactiveCommand<Unit, Unit> CreateAccount { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        private bool _showError = false;
        public bool ShowError { 
            get => _showError; 
            set 
            { 
                _showError = value;
                this.RaisePropertyChanged(); 
            }
        }
        public RegistrationViewModel(IScreen screen) : base(screen)
        {
            CreateAccount = ReactiveCommand.Create(() => {
                
            });
        }
    }
}

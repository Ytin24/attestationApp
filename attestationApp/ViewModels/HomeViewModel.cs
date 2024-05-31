using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using attestationApp.Models;
using attestationApp.Services;
using System;
using Autofac.Core;

namespace attestationApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _fullName;
        private string _lastName;
        private string _patronymic;
        private DateTimeOffset _birthdate;
        private Gender _selectedGender;
        private Test _selectedTest;
        private readonly IAttestationTasksService _service;
        TestViewModel testView;
        public HomeViewModel(IAttestationTasksService service, IScreen screen, TestViewModel testView) : base(screen)
        {
            _service = service;
            this.testView = testView;
            Genders = new ObservableCollection<Gender>();
            Tests = new ObservableCollection<Test>();
            SubmitCommand = ReactiveCommand.CreateFromTask(SubmitAsync);
            LoadDataCommand = ReactiveCommand.CreateFromTask(LoadDataAsync);
            LoadDataCommand.Execute().Subscribe();
        }

        public string FullName
        {
            get => _fullName;
            set => this.RaiseAndSetIfChanged(ref _fullName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        public string Patronymic
        {
            get => _patronymic;
            set => this.RaiseAndSetIfChanged(ref _patronymic, value);
        }

        public DateTimeOffset Birthdate
        {
            get => _birthdate;
            set => this.RaiseAndSetIfChanged(ref _birthdate, value);
        }

        public ObservableCollection<Gender> Genders { get; }
        public Gender SelectedGender
        {
            get => _selectedGender;
            set => this.RaiseAndSetIfChanged(ref _selectedGender, value);
        }

        public ObservableCollection<Test> Tests { get; }
        public Test SelectedTest
        {
            get => _selectedTest;
            set => this.RaiseAndSetIfChanged(ref _selectedTest, value);
        }

        public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }

        private async Task LoadDataAsync()
        {
            var genders = await _service.GetGendersAsync();
            foreach (var gender in genders)
            {
                Genders.Add(gender);
            }

            var tests = await _service.GetTestsAsync();
            foreach (var test in tests)
            {
                Tests.Add(test);
            }
        }

        private async Task SubmitAsync()
        {
            var student = new Student
            {
                FullName = FullName,
                LastName = LastName,
                Patronymic = Patronymic,
                Birthdate = Birthdate.LocalDateTime,
                GenderId = SelectedGender.Id
            };

            student = await _service.CreateStudentAsync(student);
            if(student == null)
            {
                throw new Exception();
            }

            _service.CurrentStudent = student;
            _service.CurrentTest = SelectedTest;
            HostScreen.Router.Navigate.Execute(testView);
        }
    }
}

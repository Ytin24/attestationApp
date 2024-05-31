using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using attestationApp.Models;
using attestationApp.Services;
using System;

namespace attestationApp.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        private readonly IAttestationTasksService _service;
        private Student _student;
        private Test _test;
        private Question _currentQuestion;
        private Answer _currentAnswer;

        CompletionViewModel completionView;

        public TestViewModel(IScreen screen, IAttestationTasksService service, CompletionViewModel completionView) : base(screen)
        {
            _service = service;
            this.completionView = completionView;
            this.WhenAnyValue(x => x._service.CurrentTest).WhereNotNull().Subscribe(async x =>
            {
                _test = x;
                LoadTestCommand.Execute().Subscribe();
            });
            this.WhenAnyValue(x => x._service.CurrentStudent).WhereNotNull().Subscribe(x =>
            {
                _student = x;
            });
            LoadTestCommand = ReactiveCommand.CreateFromTask(LoadTestAsync);
            SubmitAnswerCommand = ReactiveCommand.CreateFromTask<Answer>(SubmitAnswerAsync);
            
        }

        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => this.RaiseAndSetIfChanged(ref _currentQuestion, value);
        }
        public Answer SelectedAnswer
        {
            get => _currentAnswer;
            set => this.RaiseAndSetIfChanged(ref _currentAnswer, value);
        }

        public ReactiveCommand<Unit, Unit> LoadTestCommand { get; }
        public ReactiveCommand<Answer, Unit> SubmitAnswerCommand { get; }

        private async Task LoadTestAsync()
        {
            CurrentQuestion = await _service.GetTaskWithPossibleAnswersAsync(_test.Id);
        }

        private async Task SubmitAnswerAsync(Answer answer)
        {
            await _service.SubmitAnswerAsync(_student.Id, CurrentQuestion.Id, SelectedAnswer.Text, SelectedAnswer.IsCorrect);
            HostScreen.Router.Navigate.Execute(completionView);
        }
    }
}

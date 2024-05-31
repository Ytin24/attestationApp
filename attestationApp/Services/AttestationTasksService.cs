using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using attestationApp.DB;
using attestationApp.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace attestationApp.Services
{
    public interface IAttestationTasksService
    {
        Student CurrentStudent { get; set; }
        Test CurrentTest { get; set; }
        Task<List<Gender>> GetGendersAsync();
        Task<List<Test>> GetTestsAsync();
        Task<Question> GetTaskWithPossibleAnswersAsync(int testId);
        Task SubmitAnswerAsync(int studentId, int questionId, string answerText, bool isCorrect);
        Task<Student> CreateStudentAsync(Student student);
    }

    public class AttestationTasksService : ReactiveObject, IAttestationTasksService
    {
        private readonly AttestationDbContext _context;
        public Student _currentStudent;

        public Student CurrentStudent
        {
            get => _currentStudent;
            set => this.RaiseAndSetIfChanged(ref _currentStudent, value);
        }

        public Test _currentTest;

        public Test CurrentTest
        {
            get => _currentTest;
            set => this.RaiseAndSetIfChanged(ref _currentTest, value);
        }

        public AttestationTasksService(AttestationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task<List<Test>> GetTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Question> GetTaskWithPossibleAnswersAsync(int testId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == testId);
        }

        public async Task SubmitAnswerAsync(int studentId, int questionId, string answerText, bool isCorrect)
        {
            var answer = new Answer
            {
                QuestionId = questionId,
                Text = answerText,
                IsCorrect = isCorrect
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            try
            {
                _context.Students.Add(student);
            
                await _context.SaveChangesAsync();

            }
            catch(Exception ex) 
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return null;
            }

            return student;
        }
    }
}

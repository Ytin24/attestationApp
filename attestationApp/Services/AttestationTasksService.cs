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
            var q = await _context.Questions.Include(q => q.Tests).FirstOrDefaultAsync(x => x.Id == questionId);
            if (q == null)
            {
                return;
            }
            
            var test = q.Tests.FirstOrDefault(x => x.QuestionId == questionId);


            var a = _context.Directions.Include(x => x.Students).ToList();
            var b = a.FirstOrDefault(x => x.Students.Any(y => y.Id == studentId));
            var c = b?.Id;

            var result = new Result()
            {
                StudentId = studentId,
                TestId = test.Id,
                MarkId = isCorrect ? 1 : 4,
                DirectionId =  _context.Directions.Include(x => x.Students).FirstOrDefault(x => x.Students.Any(y => y.Id == studentId))?.Id,
            };
            await _context.Results.AddAsync(result);

            await _context.SaveChangesAsync();
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            var a = _context.Students.ToList();
            var x = _context.Students.AsNoTracking().ToList().FirstOrDefault(student.Equals);
            if (x != null)
            {
                return x;
            }

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

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
using System.Reflection;
using Avalonia.Controls; // Для использования StackPanel и TextBlock
using Avalonia.Layout; // Для настройки выравнивания и ориентации
using Avalonia.Media; // Для цветов и шрифтов
using Material.Icons.Avalonia; // Предполагая, что библиотека иконок подключена
using Material.Icons;
using Avalonia.ReactiveUI;
using attestationApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace attestationApp
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var viewModelName = viewModel.GetType().Name;

            // Предполагаем, что View имеет то же имя, что и ViewModel, но без "Model" и с суффиксом "View"
            var viewName = viewModelName.Replace("ViewModel", "View");

            // Ищем соответствующий тип View
            var viewType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == viewName);
            if (viewType == null)
            {
                return default;
            }

            // Создаем экземпляр View
            var view = Activator.CreateInstance(viewType);
            if (view == null)
            {
                throw new InvalidOperationException($"Не удалось создать представление для {viewModelName}");
            }

            // Устанавливаем DataContext
            ((UserControl)view).DataContext = viewModel;
            return (IViewFor?)view;
        }
    }
}
//CREATE TABLE Gender (
//    ID INT PRIMARY KEY IDENTITY,
//    Title NVARCHAR(50) NOT NULL
//);

//CREATE TABLE Student (
//    ID INT PRIMARY KEY IDENTITY,
//    FullName NVARCHAR(100) NOT NULL,
//    LastName NVARCHAR(100) NOT NULL,
//    Patronymic NVARCHAR(100),
//    Birthdate DATE NOT NULL,
//    GenderID INT,
//    FOREIGN KEY (GenderID) REFERENCES Gender(ID)
//);

//CREATE TABLE Mark (
//    ID INT PRIMARY KEY IDENTITY,
//    Title NVARCHAR(50) NOT NULL
//);

//CREATE TABLE Direction (
//    ID INT PRIMARY KEY IDENTITY,
//    Title NVARCHAR(50) NOT NULL
//);

//CREATE TABLE StudentDirection (
//    StudentID INT,
//DirectionID INT,
//    PRIMARY KEY (StudentID, DirectionID),
//    FOREIGN KEY(StudentID) REFERENCES Student(ID),
//    FOREIGN KEY(DirectionID) REFERENCES Direction(ID)
//);

//CREATE TABLE Question (
//    ID INT PRIMARY KEY IDENTITY,
//    Question NVARCHAR(500) NOT NULL
//);

//CREATE TABLE[Option] (
//    ID INT PRIMARY KEY IDENTITY,
//    Title NVARCHAR(200) NOT NULL
//);

//CREATE TABLE Test (
//    ID INT PRIMARY KEY IDENTITY,
//    OptionID INT,
//QuestionID INT,
//    FOREIGN KEY (OptionID) REFERENCES[Option](ID),
//    FOREIGN KEY(QuestionID) REFERENCES Question(ID)
//);

//CREATE TABLE Answer (
//    ID INT PRIMARY KEY IDENTITY,
//    QuestionID INT,
//Text NVARCHAR(500) NOT NULL,
//    IsCorrect BIT NOT NULL,
//    FOREIGN KEY (QuestionID) REFERENCES Question(ID)
//);

//CREATE TABLE Result (
//    ID INT PRIMARY KEY IDENTITY,
//    StudentID INT,
//    TestID INT,
//MarkID INT,
//DirectionID INT,
//    Description NVARCHAR(1000),
//    FOREIGN KEY(StudentID) REFERENCES Student(ID),
//    FOREIGN KEY(TestID) REFERENCES Test(ID),
//    FOREIGN KEY(MarkID) REFERENCES Mark(ID),
//    FOREIGN KEY(DirectionID) REFERENCES Direction(ID)
//);



//--Вставка данных в таблицу Gender
//INSERT INTO Gender (Title) VALUES
//('Мужской'),
//('Женский');

//--Вставка данных в таблицу Student
//INSERT INTO Student (FullName, LastName, Patronymic, Birthdate, GenderID) VALUES
//('Иван', 'Иванов', 'Иванович', '1999-01-01', 1),
//('Мария', 'Петрова', 'Петровна', '2000-02-02', 2),
//('Сергей', 'Сидоров', 'Сергеевич', '1998-03-03', 1);

//--Вставка данных в таблицу Mark
//INSERT INTO Mark (Title) VALUES
//('Отлично'),
//('Хорошо'),
//('Удовлетворительно'),
//('Неудовлетворительно');

//--Вставка данных в таблицу Direction
//INSERT INTO Direction (Title) VALUES
//('Информатика'),
//('Математика'),
//('Физика');

//--Вставка данных в таблицу StudentDirection
//INSERT INTO StudentDirection (StudentID, DirectionID) VALUES
//(1, 1),
//(2, 2),
//(3, 3);

//--Вставка данных в таблицу Question
//INSERT INTO Question (Question) VALUES
//('Что такое алгоритм?'),
//('Какова скорость света?'),
//('Сколько будет 2 + 2?'),
//('Что такое переменная?'),
//('Какой язык программирования используется для разработки веб-приложений?');

//--Вставка данных в таблицу Option
//INSERT INTO [Option] (Title)VALUES
//('Правильный'),
//('Неправильный');

//--Вставка данных в таблицу Test
//INSERT INTO Test (OptionID, QuestionID) VALUES
//(1, 1),
//(2, 2),
//(1, 3),
//(1, 4),
//(1, 5);

//--Вставка данных в таблицу Answer
//-- Вопрос 1: Что такое алгоритм?
//INSERT INTO Answer (QuestionID, Text, IsCorrect) VALUES
//(1, 'Набор инструкций для выполнения задачи', 1),  --Правильный ответ
//(1, 'Физическое устройство', 0),                  --Неправильный ответ
//(1, 'Математическая операция', 0),                --Неправильный ответ
//(1, 'Метод программирования', 0); --Неправильный ответ

//-- Вопрос 2: Какова скорость света?
//INSERT INTO Answer (QuestionID, Text, IsCorrect) VALUES
//(2, '299 792 458 м/с', 1),                        --Правильный ответ
//(2, '150 000 000 м/с', 0),                        --Неправильный ответ
//(2, '300 000 000 м/с', 0),                        --Неправильный ответ
//(2, '250 000 000 м/с', 0); --Неправильный ответ

//-- Вопрос 3: Сколько будет 2 + 2?
//INSERT INTO Answer (QuestionID, Text, IsCorrect) VALUES
//(3, '4', 1),                                      --Правильный ответ
//(3, '3', 0),                                     --Неправильный ответ
//(3, '5', 0),                                     --Неправильный ответ
//(3, '6', 0); --Неправильный ответ

//-- Вопрос 4: Что такое переменная?
//INSERT INTO Answer (QuestionID, Text, IsCorrect) VALUES
//(4, 'Именованная область памяти для хранения данных', 1), --Правильный ответ
//(4, 'Часть процессора', 0),                    --Неправильный ответ
//(4, 'Устройство ввода', 0),                    --Неправильный ответ
//(4, 'Программное обеспечение', 0); --Неправильный ответ

//-- Вопрос 5: Какой язык программирования используется для разработки веб-приложений?
//INSERT INTO Answer (QuestionID, Text, IsCorrect) VALUES
//(5, 'JavaScript', 1),                          --Правильный ответ
//(5, 'C++', 0),                                 --Неправильный ответ
//(5, 'Python', 0),                              --Неправильный ответ
//(5, 'Java', 0); --Неправильный ответ

//-- Вставка данных в таблицу Result
//INSERT INTO Result (StudentID, TestID, MarkID, DirectionID, Description) VALUES
//(1, 1, 1, 1, 'Отличное знание информатики'),
//(2, 2, 2, 2, 'Хорошее знание математики'),
//(3, 3, 3, 3, 'Удовлетворительное знание физики');
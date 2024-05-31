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

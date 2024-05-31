using attestationApp.DB;
using attestationApp.ViewModels;
using attestationApp.Views;
using Autofac;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System.Linq;
using System.Reflection;

namespace attestationApp
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacRegisterAllMVVM>();
            builder.RegisterType<AttestationDbContext>()
                .AsSelf()
                .SingleInstance();

            AutofacDependencyResolver resolver = new AutofacDependencyResolver(builder);
            Locator.SetLocator(resolver);
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();

            var container = builder.Build();
            resolver.SetLifetimeScope(container);
        }
    }
    public class AutofacRegisterAllMVVM : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Регистрация всех ViewModel
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("ViewModel"))
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();

            // Регистрация всех сервисов
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Bizi.Domain;
using Caliburn.Micro;

namespace Bizi.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        SimpleContainer _container = new SimpleContainer();

        public Bootstrapper() => Initialize();

        protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<ShellViewModel>();

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ISplashScreenViewModel, SplashScreenViewModel>();

            GetType().Assembly.GetTypes()
                .Where(vmtype => vmtype.IsClass && vmtype.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(vmtype => _container.RegisterPerRequest(vmtype, vmtype.ToString(), vmtype));         
        }

        protected override void BuildUp(object instance) => _container.BuildUp(instance);

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
    }
}

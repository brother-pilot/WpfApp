using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp.DI;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.AddSingleton<ICsvLoader, CsvLoader>();
            services.AddSingleton<IValidator<Interval>, Validator<Interval>>();
            services.AddSingleton<ISummaryService, SummaryService>();
            services.AddTransient<MainViewModel>();

            var provider = services.BuildServiceProvider();

            var vm = provider.GetRequiredService<MainViewModel>();
            var window = new MainWindow { DataContext = vm };
            window.Show();

        }

    }
}

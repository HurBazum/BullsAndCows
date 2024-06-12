using BullsAndCows.DAL;
using BullsAndCows.DAL.Repositories;
using BullsAndCows.DAL.Entities;
using BullsAndCows.Interfaces;
using BullsAndCows.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using BullsAndCows.BLL;

namespace BullsAndCows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static IHost? _host;

        public static IHost Host => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            host.StartAsync().ConfigureAwait(false);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var host = Host;
            base.OnExit(e);

            host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _host = null;
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services) => services
            .AddDbContext<BullsAndCowsContext>(ServiceLifetime.Singleton)
            .AddTransient<IRepository<ScoreRecord>, ScoreRepository>()
            .AddTransient<IEntityService<ScoreViewModel>, EntityService<ScoreViewModel>>()
            .AddViewModels();
    }

}

using Calculator.CandidatesAPI.Service;
using Calculator.Data;
using Calculator.Data.Repository;
using Calculator.Domain.AuthenticationServices;
using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Navigation;
using Calculator.UI.ViewModels;
using Calculator.UI.ViewModels.Factories;
using Calculator.UI.Views;
using Calculator.UsersAPI.Service;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;

namespace Calculator.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<CalculatorDbContextFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICandidateService, CandidateService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<ICalculatorViewModelAbstractFactory, CalculatorViewModelAbstractFactory>();
            services.AddSingleton<ICalculatorViewModelFactory<LoginViewModel>, LoginViewModelFactory>();
            services.AddSingleton<ICalculatorViewModelFactory<ElectionStatisticsViewModel>, ElectionStatisticsViewModelFactory>();
            services.AddSingleton<ICalculatorViewModelFactory<ElectionVoteViewModel>, ElectionVoteViewModelFactory>();

            services.AddSingleton<IGenericRepository<PoliticalParty>, GenericRepository<PoliticalParty>>();
            services.AddSingleton<IGenericRepository<Vote>, GenericRepository<Vote>>();
            services.AddSingleton<IGenericRepository<UnauthorizedAttempt>, GenericRepository<UnauthorizedAttempt>>();
            services.AddSingleton<IGenericRepository<Candidate>, GenericRepository<Candidate>>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICandidateProvider, CandidatesProvider>();

            services.AddSingleton<IDesignTimeDbContextFactory<CalculatorDbContext>, CalculatorDbContextFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}

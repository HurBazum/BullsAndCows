using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCows.ViewModels
{
    public static class ViewModelRegister
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddTransient<MainViewModel>()
            .AddTransient<ScoreViewModel>()
            .AddTransient<ListScoreViewModel>()
            .AddTransient<GameViewModel>();
    }
}
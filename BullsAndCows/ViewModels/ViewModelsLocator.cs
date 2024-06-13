using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCows.ViewModels
{
    public class ViewModelsLocator
    {
        public static MainViewModel MainViewModel => App.Host.Services.GetRequiredService<MainViewModel>();

        public static ScoreViewModel ScoreViewModel => App.Host.Services.GetRequiredService<ScoreViewModel>();

        public static ListScoreViewModel ListScoreViewModel => App.Host.Services.GetRequiredService<ListScoreViewModel>();
        public static GameViewModel GameViewModel => App.Host.Services.GetRequiredService<GameViewModel>();
    }
}
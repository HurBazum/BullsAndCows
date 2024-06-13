using BullsAndCows.Infrastructure;
using BullsAndCows.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace BullsAndCows.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _title = "Bulls and cows";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private BaseViewModel _viewModel = App.Host.Services.GetRequiredService<ListScoreViewModel>();

        public BaseViewModel ViewModel
        {
            get => _viewModel;
            set => Set(ref _viewModel, value);
        }


        #region Cmds

        #region to rating

        private ICommand? lookAtRatingCmd;
        public ICommand LookAtRatingCmd => lookAtRatingCmd ??= new LambdaCommand(LookAtRatingCmdExecute, CanLookAtRatingCmdExecuted);


        private void LookAtRatingCmdExecute(object parameter)
        {
            ViewModel = App.Host.Services.GetRequiredService<ListScoreViewModel>();
        }

        private bool CanLookAtRatingCmdExecuted(object parameter)
        {
            if(ViewModel is ListScoreViewModel)
            {
                return false;
            }

            return true;
        }


        #endregion


        #region to game

        private ICommand? playGameCmd;
        public ICommand PlayGameCmd => playGameCmd ??= new LambdaCommand(PlayGameCmdExecute, CanPlayGameCmdExecuted);

        private void PlayGameCmdExecute(object parameter)
        {
            ViewModel = App.Host.Services.GetRequiredService<GameViewModel>();
        }

        private bool CanPlayGameCmdExecuted(object parameter)
        {
            if(ViewModel is GameViewModel)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
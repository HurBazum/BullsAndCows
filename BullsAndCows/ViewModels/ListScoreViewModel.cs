using BullsAndCows.Interfaces;
using BullsAndCows.ViewModels.Base;
using System.Collections.ObjectModel;

namespace BullsAndCows.ViewModels
{
    public class ListScoreViewModel : BaseViewModel
    {
        private string _title = "Топ игроков";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private readonly IEntityService<ScoreViewModel> _entityService;

        public ObservableCollection<ScoreViewModel> Scores { get; set; } = [];
        private async void InitializeScores()
        {
            foreach(var item in await _entityService.GetAll())
            {
                Scores.Add(item);
            }
        }

        public ListScoreViewModel(IEntityService<ScoreViewModel> entityService)
        {
            _entityService = entityService;
            InitializeScores();
        }
    }
}
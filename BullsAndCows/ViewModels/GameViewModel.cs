using BullsAndCows.Interfaces;
using BullsAndCows.ViewModels.Base;

namespace BullsAndCows.ViewModels
{
    public class GameViewModel(IEntityService<ScoreViewModel> entityService) : BaseViewModel
    {
        private readonly IEntityService<ScoreViewModel> _entityService = entityService;


    }
}

using BullsAndCows.ViewModels.Base;
using BullsAndCows.Interfaces;

namespace BullsAndCows.ViewModels
{
    public class ScoreViewModel : BaseViewModel, IEntity
    {
        private int id;
        private string player = string.Empty;
        private double score = 0;
        public int Id
        {
            get => id;
            set => Set(ref id, value);
        }
        public string Player
        {
            get => player;
            set => Set(ref player, value);
        }
        public double Score
        {
            get => score;
            set => Set(ref score, value);
        }
    }
}
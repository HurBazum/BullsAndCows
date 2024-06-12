using BullsAndCows.Interfaces;

namespace BullsAndCows.DAL.Entities
{
    public class ScoreRecord : IEntity
    {
        public int Id { get; set; }
        public string Player { get; set; }   
        public double Score { get; set; }
    }
}
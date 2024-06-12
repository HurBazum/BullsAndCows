using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public double Score { get; set; }
    }
}

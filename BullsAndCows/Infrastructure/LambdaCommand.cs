using BullsAndCows.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Infrastructure
{
    internal class LambdaCommand(Action<object> execute, Func<object, bool> canExecute) : Command
    {
        private Action<object> _execute = execute;
        private Func<object, bool> _canExecute = canExecute;
        public override void Execute(object? parameter) => _execute(parameter);
        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
    }
}

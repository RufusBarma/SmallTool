using System;
using System.Linq;
using MoreLinq;

namespace SmallTool.Models.BranchCommands
{
    public class ChainCommand : IBranchCommand
    {
        private readonly Func<BranchTuple, bool> _canExecute;
        private readonly IBranchCommand[] _commands;

        public ChainCommand(params IBranchCommand[] commands)
        {
            _commands = commands;
        }
        
        public ChainCommand(Func<BranchTuple, bool> canExecute, params IBranchCommand[] commands)
        {
            _canExecute = canExecute;
            _commands = commands;
        }
        
        public void Execute(BranchTuple tuple)
        {
            _commands.ForEach(command => command.Execute(tuple));
        }

        public bool CanExecute(BranchTuple tuple)
        {
            if (_canExecute == null)
                return _commands.All(command => command.CanExecute(tuple));
            else
                return _canExecute(tuple);
        }
    }
}
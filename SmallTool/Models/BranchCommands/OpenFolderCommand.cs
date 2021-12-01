using System;
using ElectronNET.API;
using LanguageExt;

namespace SmallTool.Models.BranchCommands
{
    public class OpenFolderCommand : IBranchCommand
    {
        private readonly Func<BranchTuple, Option<string>> _getPath;
        
        public OpenFolderCommand(Func<BranchTuple, Option<string>> getPath)
        {
            _getPath = getPath;
        }
        
        public void Execute(BranchTuple tuple)
        {
            var path = _getPath(tuple).IfNone(() =>
                throw new ArgumentException("Can't execute for this branch tuple"));
            Electron.Shell.OpenPathAsync(path);
        }

        public bool CanExecute(BranchTuple tuple)
        {
            return _getPath(tuple).IsSome;
        }
    }
}
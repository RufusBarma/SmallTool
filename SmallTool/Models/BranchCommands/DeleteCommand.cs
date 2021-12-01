namespace SmallTool.Models.BranchCommands
{
    public class DeleteCommand : IBranchCommand
    {
        public void Execute(BranchTuple tuple)
        {
            throw new System.NotImplementedException();
        }

        public bool CanExecute(BranchTuple tuple)
        {
            return tuple.LocalBuild.IsSome;
        }
    }
}
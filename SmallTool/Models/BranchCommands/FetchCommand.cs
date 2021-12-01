namespace SmallTool.Models.BranchCommands
{
    public class FetchCommand : IBranchCommand
    {
        public void Execute(BranchTuple tuple)
        {
            throw new System.NotImplementedException();
        }

        public bool CanExecute(BranchTuple tuple)
        {
            return tuple.RemoteBuild.IsSome;
        }
    }
}
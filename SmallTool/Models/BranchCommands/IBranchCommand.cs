namespace SmallTool.Models.BranchCommands
{
    public interface IBranchCommand
    {
        public void Execute(BranchTuple tuple);
        public bool CanExecute(BranchTuple tuple);
    }
}
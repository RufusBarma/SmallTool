using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmallTool.Models.BranchCommands
{
    public class DeleteCommand : IBranchCommand
    {
        public void Execute(BranchTuple tuple)
        {
            tuple.LocalBuild.IfSome(branch =>
            {
                foreach (var buildPath in branch.Builds.Select(build => build.Path))
                    new Task(() => Directory.Delete(buildPath, true)).Start();
            });
        }

        public bool CanExecute(BranchTuple tuple)
        {
            return tuple.LocalBuild.IsSome;
        }
    }
}
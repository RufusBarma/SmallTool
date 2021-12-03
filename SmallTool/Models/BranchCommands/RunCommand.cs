using System.Diagnostics;
using System.IO;
using ElectronNET.API;
using MoreLinq;
using SmallTool.Extensions;

namespace SmallTool.Models.BranchCommands
{
    public class RunCommand: IBranchCommand
    {
        public void Execute(BranchTuple tuple)
        {
            tuple.LocalBuild.IfSome(branch => RunBuild(branch.GetFresh()));
        }

        public bool CanExecute(BranchTuple tuple) => tuple.LocalBuild.Match(b => b.GetFresh() != null, false);

        private void RunBuild(Build build)
        {
            var exePath = Path.Combine(build.Path, "DemoGames", "DemoGames.exe");
            //var process = Process.Start(exePath);
            Electron.WindowManager.BrowserWindows.ForEach(window => window.Hide());
        }
    }
}
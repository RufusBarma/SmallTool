using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmallTool.Models;

namespace SmallTool.Extensions
{
    public class DirectoryAnalyzer
    {
        public static IEnumerable<Build> ScanFolder(string rootPath)
        {
            var branches = new Dictionary<string, Branch>();
            return ScanFolder(rootPath, branches);
        }

        private static IEnumerable<Build> ScanFolder(string rootPath, Dictionary<string, Branch> branches)
        {
            var currentDirectory = new DirectoryInfo(rootPath);
            if (!currentDirectory.Exists) yield break;
            if (currentDirectory.GetFiles().Any(f => f.Name == "metaCI.txt"))
                yield return ScanBuildFolder(rootPath, branches);
            else
            {
                var builds = currentDirectory.GetDirectories().Select(x => x.FullName).SelectMany(x=>ScanFolder(x, branches));
                foreach (var build in builds)
                    yield return build;
            }
        }

        public static Build ScanBuildFolder(string path, Dictionary<string, Branch> branches)
        {
            var directory = new DirectoryInfo(path);
            var metaFile = directory.GetFiles().First(f => f.Name == "metaCI.txt");
            var metaInfo = File.ReadAllLines(metaFile.FullName);
            var metaInfoFirstLineValues = metaInfo[0].Split(' ');
            var branch = GetBranch(metaInfoFirstLineValues[0], branches);

            var build = new Build
            {
                Path = path,
                CreatedTime = directory.CreationTime,
                IsUserBuild = directory.GetDirectories().Any(d => d.Name == "Bundles"),
                Branch = branch,
                CommitHash = metaInfoFirstLineValues[1]
            };
            
            branch.Builds.Add(build);
            
            return build;
        }

        private static Branch GetBranch(string name, Dictionary<string, Branch> branches)
        {
            if (branches.TryGetValue(name, out var branch))
            {
                return branch;
            }
            else
            {
                var newBranch = new Branch(name);
                branches.Add(name, newBranch);
                return newBranch;
            }
        }
    }
}
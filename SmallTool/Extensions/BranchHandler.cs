using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LanguageExt;
using Microsoft.Extensions.Configuration;
using MoreLinq.Extensions;
using SmallTool.Models;

namespace SmallTool.Extensions
{
    public class BranchHandler
    {
        public ImmutableList<BranchTuple> BranchTuples { get; private set; }
        public ImmutableList<Branch> RemoteBranches { get; private set; }
        public ImmutableList<Branch> LocalBranches { get; private set; }

        private IConfiguration config;
        
        public BranchHandler(IConfiguration config)
        {
            this.config = config;
            RefreshBuilds();
        }

        public void RefreshBuilds()
        {
            var remotePath = config.GetSection("BuildRootDirectory")["RemoteDirectory"];
            var localPath = config.GetSection("BuildRootDirectory")["LocalDirectory"];
            RemoteBranches = GetBuildsInBranches(remotePath).ToImmutableList();
            LocalBranches = GetBuildsInBranches(localPath).ToImmutableList();
            BranchTuples = CreateTuples(LocalBranches, RemoteBranches).ToImmutableList();
        }

        private IEnumerable<Branch> GetBuildsInBranches(string path)
        {
            return DirectoryAnalyzer.ScanFolder(path).Select(build => build.Branch).Distinct();
        }

        private IEnumerable<BranchTuple> CreateTuples(IEnumerable<Branch> local, IEnumerable<Branch> remote)
        {
            var tuples = local.FullJoin(remote, 
                pair => pair.Name, 
                left => new BranchTuple(left, Option<Branch>.None), 
                right => new BranchTuple(Option<Branch>.None, right),
                (left, right) => new BranchTuple(left, right));
            return tuples;
        }
    }
}
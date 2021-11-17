using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SmallTool.Models;

namespace SmallTool.Extensions
{
    public class BranchHandler
    {
        public ImmutableList<BranchTuple> BuildTuples { get; private set; }
        public ImmutableList<Branch> RemoteBuilds { get; private set; }
        public ImmutableList<Branch> LocalBuilds { get; private set; }

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
            RemoteBuilds = GetBuildsInBranches(remotePath).ToImmutableList();
            LocalBuilds = GetBuildsInBranches(localPath).ToImmutableList();
        }

        private IEnumerable<Branch> GetBuildsInBranches(string path)
        {
            throw new NotImplementedException();
            // return DirectoryAnalyzer.ScanFolder(path)
            //     .GroupBy(b => b.BranchName)
            //     .ToDictionary(b => b.Key, b => b.ToList());
        }

        private List<BranchTuple> CreateTuples(Dictionary<string, List<Build>> local,
            Dictionary<string, List<Build>> remote)
        {
            var tuples = new List<BranchTuple>();
            return tuples;
        }
    }
}
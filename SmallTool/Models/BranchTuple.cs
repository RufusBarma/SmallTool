using System;
using System.Linq;
using LanguageExt;
using LanguageExt.SomeHelp;

namespace SmallTool.Models
{
    public record BranchTuple(Option<Branch> LocalBuild, Option<Branch> RemoteBuild);

    public enum BranchStatus
    {
        Manual,
        Actual,
        Outdated,
        Deleted,
        New
    }
    
    public static class BranchTupleExtension
    {
        public static BranchStatus GetStatus(this BranchTuple tuple)
        {
            // if (tuple.LocalBuild.IsNone && tuple.RemoteBuild.IsNone)
                return BranchStatus.Manual;
        }
    }
}
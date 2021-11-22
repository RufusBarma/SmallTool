using LanguageExt;

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
}
using LanguageExt;

namespace SmallTool.Models
{
    public record BranchTuple(Option<Branch> LocalBuild, Option<Branch> RemoteBuild);
}
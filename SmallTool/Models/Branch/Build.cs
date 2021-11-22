using System;

namespace SmallTool.Models
{
    public record Build
    {
        public string Path { get; set; }
        public DateTime CreatedTime { get; set; }
        public Branch Branch { get; set; }
        public string CommitHash { get; set; }
        public bool IsUserBuild { get; set; }
    }
}
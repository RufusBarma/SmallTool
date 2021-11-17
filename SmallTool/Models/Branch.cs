using System.Collections.Generic;

namespace SmallTool.Models
{
    public record Branch
    {
        public Branch(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<Build> Builds { get; set; } = new();
    }
}
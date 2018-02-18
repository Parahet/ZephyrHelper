using System.Collections.Generic;

namespace ZephyrHelper.Entities
{
    public class ProjectInfo
    {
        public string Expand;
        public string Self;
        public string Id;
        public string Key;
        public string Description;
        public Lead Lead;
        public List<Component> Components;
        public List<Version> Versions;
    }
}

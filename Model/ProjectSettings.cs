using System;

namespace QuestProject
{
    public class ProjectSettings
    {
        public string Projectname { get; set}
        public long Description { get; set; }
        public string Path { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public float Maxresults { get; set; }
        public bool Notifications { get; set; }
        public string Projecttype { get; set; }
    }
}

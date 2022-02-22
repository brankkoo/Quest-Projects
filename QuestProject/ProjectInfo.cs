using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestProject
{
    //Base Class
    public class ProjectInfo
    {
        private string Projectname;
        private string Description;
        private string Path;
        private DateTime Startdate;
        private DateTime Enddate;
        private float Maxresults;
        private bool? Notifications;
        private int Projecttype;

        public string projectName
        {
            get { return Projectname; }
            set { Projectname = value; }
        }

        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        public string path
        {
            get { return Path; }
            set { Path = value; }
        }

        public DateTime startDate
        {
            get { return Startdate; }
            set { Startdate = value; }
        }

        public DateTime endDate
        {
            get { return Enddate; }
            set { Enddate = value; }
        }
        public float maxResults
        {
            get { return Maxresults; }
            set { Maxresults = value; }
        }

        public bool? notification
        {
            get { return Notifications; }
            set { Notifications = value; }
        }
        public int projectType
        {
            get { return Projecttype; }
            set { Projecttype = value; }
        }

    }
}

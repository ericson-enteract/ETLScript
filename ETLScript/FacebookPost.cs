using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETLScript
{
    class FacebookPost
    {
        public class From
        {
            public string name { get; set; }
            public string id { get; set; }
            public string category { get; set; }
        }

        public class Datum2
        {
            public string message { get; set; }
            public From from { get; set; }
            public string id { get; set; }
            public string created_time { get; set; }
        }

        public class Paging
        {
            public string next { get; set; }
        }

        public class Comments
        {
            public List<Datum2> data { get; set; }
            public Paging paging { get; set; }
        }

        public class Datum
        {
            public string message { get; set; }
            public string id { get; set; }
            public string created_time { get; set; }
            public Comments comments { get; set; }
        }

        public class Paging2
        {
            public string previous { get; set; }
            public string next { get; set; }
        }

        public class Posts
        {
            public List<Datum> data { get; set; }
            public Paging2 paging { get; set; }
        }

        public class RootObject
        {
            public string id { get; set; }
            public Posts posts { get; set; }
        }
    }
}

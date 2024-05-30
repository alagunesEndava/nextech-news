using System;
using System.Collections.Generic;

namespace nextech.news.Core.Model
{
    public class User
    {
        public string Id { get; set; }
        public TimeSpan Created { get; set; }
        public int Karma { get; set; }
        public string About { get; set; }
        public List<int> Submitted { get; set; }
     }
}

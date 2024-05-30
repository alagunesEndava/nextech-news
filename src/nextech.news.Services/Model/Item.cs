﻿using System;
using System.Collections.Generic;

namespace Nextech.news.Core.Model
{
    public class Item
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public enum Type
        {
           Job, Story, Comment, Poll, Pollopt
        }
        public string By { get; set; }
        //unix time
        public long Time { get; set; }
        public string Text { get; set; }
        public bool Dead { get; set; }
        public int Parent{ get; set; }
        public List<int> Poll{ get; set; }
        public List<int> Kids { get; set; }
        public string Url { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public List<int> Parts { get; set; }
        public int Descendants { get; set; }





    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMaterial.Models
{

 
    public class Speaker
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string bio { get; set; }
        public string tagLine { get; set; }
        public string profilePicture { get; set; }
        public Session[] sessions { get; set; }
        public bool isTopSpeaker { get; set; }
        public Link[] links { get; set; }
        public object[] questionAnswers { get; set; }
        public object[] categories { get; set; }
    }

    public class Session
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Link
    {
        public string title { get; set; }
        public string url { get; set; }
        public string linkType { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCodeAPI.Models
{
    
    public class TimelineTweet
    {

        public TimelineTweet(string username, string screenname, string profileImage, string tweetContent, int retweets, DateTime tweetDate, string tweetId)
        {
            Username = username;
            Screenname = screenname;
            ProfileImage = profileImage;
            TweetContent = tweetContent;
            Retweets = retweets;
            TweetDate = tweetDate;
            TweetId = tweetId;
        }

        public string Username { get; set; }

        public string Screenname { get; set; }

        public string ProfileImage { get; set; }

        public string TweetContent { get; set; }

        public int Retweets { get; set; }

        public DateTime TweetDate { get; set; }

        public string TweetId { get; set; }
    }
}

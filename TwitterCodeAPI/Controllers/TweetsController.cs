using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterCodeAPI.Models;

namespace TwitterCodeAPI.Controllers
{

    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        public TweetsController()
        {
        }

        const string CONSUMER_KEY = "Gui8jNqZ60AVlIV0UGTypJ7aq";
        const string CONSUMER_SECRET = "G0iUUYSYVYa4Y3PyyW7s915633tz4admTudfOTtGkoZwq4wjBp";
        
        // GET api/tweets
        [HttpGet]
        public IEnumerable<TimelineTweet> Get()
        {
            Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, "1273228404-0tdiu5Pwsgu2zt0xlfrlS9G38Q060UKEri2rupf", "iRPHq6jhq67KgFNtGLSZTo7u1ORx6r2KsqfYqts3mXJlH");
            List<TimelineTweet> tweets = new List<TimelineTweet>();

            var userTimeline = Timeline.GetUserTimeline("salesforce",10);

            foreach (var timelineTweet in userTimeline)
            {
                var tweetModel = new TimelineTweet(
                    timelineTweet.CreatedBy.Name, 
                    timelineTweet.CreatedBy.ScreenName, 
                    timelineTweet.CreatedBy.ProfileBackgroundImageUrl, 
                    timelineTweet.FullText, 
                    timelineTweet.RetweetCount, 
                    timelineTweet.CreatedAt,
                    timelineTweet.IdStr);

                tweets.Add(tweetModel);
            }

            return tweets;
        }
    }
}

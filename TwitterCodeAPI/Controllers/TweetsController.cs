using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TwitterCodeAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TwitterCodeAPI.Controllers
{

    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        const string CONSUMER_KEY = "Gui8jNqZ60AVlIV0UGTypJ7aq";
        const string CONSUMER_SECRET = "G0iUUYSYVYa4Y3PyyW7s915633tz4admTudfOTtGkoZwq4wjBp";
        public string bearerToken { get; set; }

        public TweetsController()
        {
            this.AppAuth();
        }

        private void AppAuth()
        {
            //get a BearerToken;
            string bearerRequest = HttpUtility.UrlEncode(CONSUMER_KEY) + ":" + HttpUtility.UrlEncode(CONSUMER_SECRET);
            //encode to base64String
            bearerRequest = Convert.ToBase64String(Encoding.UTF8.GetBytes(bearerRequest));

            //build web request
            WebRequest request = WebRequest.Create("https://api.twitter.com/oauth2/token");
            request.Headers.Add("Authorization", "Basic " + bearerRequest);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

            //stream web request to get auth token
            string requestContent = "grant_type=client_credentials";
            byte[] bytearrayRequestContent = Encoding.UTF8.GetBytes(requestContent);
            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytearrayRequestContent, 0, bytearrayRequestContent.Length);
            requestStream.Close();

            //setup a jsonObject to get the respone token
            string responseJson = string.Empty;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.Stream responseStream = response.GetResponseStream();
                responseJson = new StreamReader(responseStream).ReadToEnd();
            }

            JObject jObject = JObject.Parse(responseJson);

            //set the bearer token
            bearerToken = jObject["access_token"].ToString();
        }
        
        
        // GET api/tweets
        [HttpGet]
        public IEnumerable<TimelineTweet> Get()
        {
            var appCreds = Auth.SetApplicationOnlyCredentials(CONSUMER_KEY, CONSUMER_SECRET, bearerToken);

            TweetinviConfig.CurrentThreadSettings.TweetMode = TweetMode.Extended;

            List<TimelineTweet> tweets = new List<TimelineTweet>();
            var userTimeline = Timeline.GetUserTimeline("salesforce",10);

            foreach (var timelineTweet in userTimeline)
            {
                var tweetModel = new TimelineTweet(
                    timelineTweet.CreatedBy.Name, 
                    timelineTweet.CreatedBy.ScreenName, 
                    timelineTweet.CreatedBy.ProfileImageUrl, 
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

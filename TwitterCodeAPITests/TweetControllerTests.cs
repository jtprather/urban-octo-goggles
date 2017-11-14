using System;
using System.Linq;
using TwitterCodeAPI.Controllers;
using Xunit;

namespace TwitterCodeAPITests
{
    public class TweetControllerTests
    {

        [Fact]
        public void TweetController_Get_ReturnsTweets()
        {
            var testController = new TweetsController();
            var result = testController.Get();
            Assert.Equal(result.Count(), 10);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogExampleApi.Controllers;

namespace BlogExampleApi.Testing
{
    [TestClass]
    public class PostUnitTest
    {
        private static string RES_ID_1 = "{\"Category\":{\"id\":1,\"title\":\"General\"},\"id\":1,\"title\":\"a sample example\",\"detail\":\"this is a good start with entity framework\",\"date_created\":null,\"date_modified\":null,\"author\":\"rajat sahu\",\"category_id\":1}";
        [TestMethod]
        public void GetPostById() {
            var post = new PostController();
            // set up request and configuration
            post.Request = new System.Net.Http.HttpRequestMessage();
            post.Configuration = new System.Web.Http.HttpConfiguration();

            // get post whose id==1
            var response = post.Get(1);
            var result = response.ExecuteAsync(new System.Threading.CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result,RES_ID_1);
        }
    }
}
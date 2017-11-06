using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogExampleApi.Controllers;
using BlogExampleApi;
using System.Web.Http;

namespace SampleTest
{
    [TestClass]
    public class UnitTest1
    {
        //private BlogEntities db = new BlogEntities();
        //private static string RES_ID_1 = "{\"Category\":{\"id\":1,\"title\":\"General\"},\"id\":1,\"title\":\"a sample example\",\"detail\":\"this is a good start with entity framework\",\"date_created\":null,\"date_modified\":null,\"author\":\"rajat sahu\",\"category_id\":1}";
        // test api/post/{id}
        [TestMethod]
        public void GetPostById()
        {
            var homeController = new HomeController();
            var result = homeController.Index();
            Assert.AreEqual("DemoApp", result);
        }

        // a sample math test
        [TestMethod]
        public void Square() {
            Assert.AreEqual(16,4 * 4);
        }
    }
}
﻿using System;
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
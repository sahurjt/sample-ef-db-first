using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogExampleApi.Controllers;
using BlogExampleApi;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Results;
using BlogExampleApi.Interface;
using FakeItEasy;
using BlogExampleApi.Models;

namespace SampleTest
{
    [TestClass]
    public class CategoryTest
    {
        private readonly static Category DEMO_CATEGORY = new Category { id = 1, title = "demo" };
        private readonly static dynamic DEMO_OBJECT = new { title = "demo_post", category_id = 1, category_title = "demo_category" };
        private readonly static IEnumerable<dynamic> DEMO_LIST = new List<dynamic>{ DEMO_OBJECT };

        [TestMethod]
        public void Get()
        {
            // Arrange
            var mockObject = A.Fake<ICategoryOperation>();
            A.CallTo(() => mockObject.GetAll()).Returns(new List<Category> { DEMO_CATEGORY});
            var controller = new CategoryController(mockObject);

            // Act
            IHttpActionResult result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Data as IEnumerable<Category>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, data.Count());
            Assert.AreEqual("demo", data.ElementAt(0).title);

        }

        [TestMethod]
        public void GetPostWhoseCategoryId()
        {
            // Act
            // Arrange
            var mockObject = A.Fake<ICategoryOperation>();
            A.CallTo(() => mockObject.GetPosts(1)).Returns(DEMO_LIST);
            var controller = new CategoryController(mockObject);
           
            // Act
            IHttpActionResult result = controller.Get(1);
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Data as IEnumerable < dynamic >;
            System.Console.WriteLine(data.ElementAt(0).title);
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, data.Count());
            Assert.AreEqual("demo_post", data.ElementAt(0).title);
            Assert.AreEqual("demo_category", data.ElementAt(0).category_title);

        }


        [TestMethod]
        public void Add()
        {
            // Act
            // Arrange
            var mockObject = A.Fake<ICategoryOperation>();
            A.CallTo(() => mockObject.Add(DEMO_CATEGORY)).Returns(true);
            var controller = new CategoryController(mockObject);

            // Act
            IHttpActionResult result = controller.Put(DEMO_CATEGORY);
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Message;
     
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Added new Category", data);

        }

        [TestMethod]
        public void Delete()
        {
            // Act
            // Arrange
            var mockObject = A.Fake<ICategoryOperation>();
            A.CallTo(() => mockObject.Delete(1)).Returns(true);
            var controller = new CategoryController(mockObject);

            // Act
            IHttpActionResult result = controller.Delete(1);
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Status;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(Response.MESSAGE_OK, data);

        }

    }
}

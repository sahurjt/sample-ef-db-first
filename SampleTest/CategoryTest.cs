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
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Category>>;
            
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.Count());
            Assert.AreEqual("demo", contentResult.Content.ElementAt(0).title);

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
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<dynamic>>;
            System.Console.WriteLine(contentResult.Content.ElementAt(0).title);
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.Count());
            Assert.AreEqual("demo_post", contentResult.Content.ElementAt(0).title);
            Assert.AreEqual("demo_category", contentResult.Content.ElementAt(0).category_title);

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
            var contentResult = result as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Added New Category", contentResult.Content);

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
            var contentResult = result as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Catgory Deleted", contentResult.Content);

        }

    }
}


using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogExampleApi.Controllers;
using BlogExampleApi;
using System.Web.Http;
using System.Net.Http;
using FakeItEasy;
using BlogExampleApi.Interface;
using System.Web.Http.Results;
using BlogExampleApi.Models;

namespace SampleTest
{
    [TestClass]
    public class PostTest
    {
    
        private readonly static Post DEMO_POST = new Post { id = 1, title = "demo", category_id = 1 };

        [TestMethod]
        public void Get()
        {
            // Arrange
            var mockObject = A.Fake<IPostOperation>();
            A.CallTo(() => mockObject.GetAll()).Returns(new List<Post> { DEMO_POST });
            var controller = new PostController(mockObject);

            // Act
            IHttpActionResult result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Data as IEnumerable<Post>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            Assert.AreEqual("demo", data.ElementAt(0).title);
        }


        [TestMethod]
        public void GetById()
        {
            // Arrange
            var mockObject = A.Fake<IPostOperation>();
            A.CallTo(() => mockObject.Get(1)).Returns(DEMO_POST);
            var controller = new PostController(mockObject);

            // Act
            IHttpActionResult result = controller.Get(1);
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Data as Post;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(data);
            Assert.AreEqual("demo", data.title);
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            var mockObject = A.Fake<IPostOperation>();
            A.CallTo(() => mockObject.AddPost(DEMO_POST)).Returns(true);
            var controller = new PostController(mockObject);

            // Act
            IHttpActionResult result = controller.Add(DEMO_POST);
            var contentResult = result as OkNegotiatedContentResult<Response>;
            var data = contentResult.Content.Data as Post;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(data);
            Assert.AreEqual("demo", data.title);
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            var mockObject = A.Fake<IPostOperation>();
            A.CallTo(() => mockObject.Update(DEMO_POST)).Returns(true);
            var controller = new PostController(mockObject);

            // Act
            IHttpActionResult result = controller.Update(DEMO_POST);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(OkResult));
        }


        [TestMethod]
        public void Delete()
        {
            // Arrange
            var mockObject = A.Fake<IPostOperation>();
            A.CallTo(() => mockObject.Delete(1)).Returns(true);
            var controller = new PostController(mockObject);

            // Act
            IHttpActionResult result = controller.Delete(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(OkResult));
        }
    }
}
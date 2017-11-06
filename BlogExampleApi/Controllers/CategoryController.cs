using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogExampleApi.Interface;
using BlogExampleApi.ModelOperation;
using BlogExampleApi.Models;

namespace BlogExampleApi.Controllers
{
    public class CategoryController : ApiController
    {
        private ICategoryOperation context;

        public CategoryController()
        {
            context = new CategoryOperations();
        }

        public CategoryController(ICategoryOperation categoryOperation)
        {
            context = categoryOperation;
        }

        /// <summary>
        /// Gel list of category available
        /// </summary>
        /// <returns>list of category</returns>
        public IHttpActionResult Get()
        {
            return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "List of category.", Data = context.GetAll() });
        }

        /// <summary>
        /// Get posts corresponding to this category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list of posts</returns>
        public IHttpActionResult Get(int id)
        {
            var result = context.GetPosts(id);
            if (result != null) return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "List of posts in this category." ,Data=result});
            return Ok(new Response { Status = Response.MESSAGE_ERROR, StatusCode = Response.STATUS_NOT_FOUND, Message = "Category not exists." });
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        // Add Category
        public IHttpActionResult Put(Category category)
        {
            if (context.Add(category)) return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "Added new Category",Data=category });
            return NotFound();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {

            if (context.Delete(id)) return Ok(new Response { Status=Response.MESSAGE_OK,StatusCode=Response.STATUS_OK,Message= "Catgory Deleted" });
            return Ok(new Response { Status = Response.MESSAGE_ERROR, StatusCode = Response.STATUS_OK, Message = "Can't delete as category not exist or its in use." });
        }
    }
}
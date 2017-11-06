using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using BlogExampleApi.Interface;
using BlogExampleApi.ModelOperation;
using BlogExampleApi.Models;

namespace BlogExampleApi.Controllers
{

    public class PostController : ApiController
    {
        Logger log = LogManager.GetCurrentClassLogger();

        private IPostOperation context;
        public PostController()
        {
            context = new PostOperations();
        }

        public PostController(IPostOperation postOperation)
        {
            context = postOperation;
        }


        /// <summary>
        /// Return all post in posts table
        /// </summary>
        /// <returns>list of post object</returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            var posts = context.GetAll();
            log.Trace("return list of posts");
            return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "List of Posts.", Data = posts });
        }


        /// <summary>
        /// Get a single post or 404
        /// </summary>
        /// <param name="id"> id of given post</param>
        /// <returns>ok or 404</returns>
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var post = context.Get(id);
            if (post == null)
            {
                log.Trace("No post found");
                return NotFound();
            }

            return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "Post detail.", Data = post });
        }

        [HttpGet]
        public IHttpActionResult Get(String data) {
            return Ok(new Response { Status = Response.MESSAGE_ERROR, StatusCode = Response.STATUS_NOT_FOUND, Message = "Route not supported.", Data =data });
        }
        /// <summary>
        /// Add new post record to database
        /// </summary>
        /// <param name="value">Post data</param>
        /// <returns>ok</returns>
        [HttpPost]
        public IHttpActionResult Add(Post value)
        {
            context.AddPost(value);
            log.Trace("add values for " + value.title);
            return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "Added Post.", Data = value });
        }

        /// <summary>
        /// Update post with given id
        /// </summary>
        /// <param name="newPost">Send post data as url-encoded form</param>
        /// <returns>always 202</returns>
        [HttpPatch]
        public IHttpActionResult Update(Post newPost)
        {
            var result = context.Update(newPost);
            if (result) return Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "Post Updated.", Data = newPost });
            else return NotFound();
        }

        /// <summary>
        /// Delete Post with this id
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>202 or 404</returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var result = context.Delete(id);
            if (result) Ok(new Response { Status = Response.MESSAGE_OK, StatusCode = Response.STATUS_OK, Message = "Post Deleted." });
            return Ok(new Response { Status = Response.MESSAGE_ERROR, StatusCode = Response.STATUS_OK, Message = "Can't delete as post not exist or its in use." });
        }


    }
}
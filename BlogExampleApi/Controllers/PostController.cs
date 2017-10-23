using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;

namespace BlogExampleApi.Controllers
{

    public class PostController : ApiController
    {
        Logger log = LogManager.GetCurrentClassLogger();
        // database instance to access table 
        private BlogEntities db = new BlogEntities();

        /// <summary>
        /// Return all post in posts table
        /// </summary>
        /// <returns>list of post object</returns>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            var posts = db.Posts;
            log.Trace("return list of posts");
            return posts;
        }

        /// <summary>
        /// Get a single post or 404
        /// </summary>
        /// <param name="id"> id of given post</param>
        /// <returns>ok or 404</returns>
        // GET api/<controller>/5
        [ResponseType(typeof(Post))]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            //var post = db.Posts.Find(id);
            var post = db.Posts.Where(p => p.id == id).SingleOrDefault();
            if (post == null)
            {
                log.Trace("No post found");
                return NotFound();
            }
            
            return Ok(post);
        }

        /// <summary>
        /// Add new post record to database
        /// </summary>
        /// <param name="value">Post data</param>
        /// <returns>ok</returns>
        [HttpPost]
        public IHttpActionResult Add(Post value)
        {
            db.Posts.Add(value);
            db.SaveChanges();
            log.Trace("add values for " + value.title);
            return Ok(value);
        }

        /// <summary>
        /// Update post with given id
        /// </summary>
        /// <param name="new_post">Send post data as url-encoded form</param>
        /// <returns>always 202</returns>
        [HttpPost]
        [Route("api/post/update")]
        public IHttpActionResult Update(Post new_post)
        {
            var post = db.Posts.Where(p => p.id == new_post.id).SingleOrDefault();
            if (post != null)
            {
                post.title = new_post.title;
                post.detail = new_post.detail;
                db.SaveChanges();
            }
            return Ok();
        }

        /// <summary>
        /// Delete Post with this id
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>202 or 404</returns>

        public IHttpActionResult Delete(int id)
        {
            var post = db.Posts.Where(p => p.id == id).SingleOrDefault();
            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            
            else return NotFound();
            return Ok();
        }

    }
}
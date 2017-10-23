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
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            var posts = db.Posts;
            log.Trace("return list of posts");
            return posts;
        }

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


        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Add(Post value)
        {
            db.Posts.Add(value);
            db.SaveChanges();
            log.Trace("add values for " + value.title);
            return Ok(value);
        }

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

        //private async Task getData() {
        //   var a=await  db.SaveChangesAsync();          
        //}
    }
}
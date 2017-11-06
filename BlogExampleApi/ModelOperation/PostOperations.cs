using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogExampleApi.Interface;

namespace BlogExampleApi.ModelOperation
{
    public class PostOperations : IPostOperation
    {
        private static BlogEntities context = new BlogEntities();
        public bool AddPost(Post post)
        {
            post.date_created = DateTime.UtcNow;
            context.Posts.Add(post);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var post = context.Posts.Find(id);
            if (post != null)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
                return true;
            }
            return false; // post not exist
        }

        public Post Get(int id)
        {
            return context.Posts.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return context.Posts;
        }

        public bool Update(Post new_post)
        {
            var post = context.Posts.Find(new_post.id);
            if (post != null)
            {
                post.title = new_post.title;
                post.detail = new_post.detail;
                post.date_modified = DateTime.UtcNow;
                if (new_post.category_id != null) post.category_id = new_post.category_id;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
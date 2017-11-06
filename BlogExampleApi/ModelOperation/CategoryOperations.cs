using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogExampleApi.Interface;

namespace BlogExampleApi.ModelOperation
{
    public class CategoryOperations : ICategoryOperation
    {
        private static BlogEntities context = new BlogEntities();
        public bool Add(Category category)
        {
            if (category != null && category.title != null && category.title != "")
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                try { context.SaveChanges(); }
                catch { return false; }
                return true;
            }
            return false;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public IEnumerable<dynamic> GetPosts(int id)
        {
            var result = context.Posts.Join(context.Categories,
                p => p.category_id,
                c => c.id,
                (p, c) => new { p.title, p.detail, p.date_created, c.id, category_id = c.id, category_title = c.title }).Where(d => d.category_id == id).ToList();
            return result;
        }
    }
}
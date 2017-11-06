using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExampleApi.Interface
{
   public interface ICategoryOperation
    {
        bool Add(Category category);

        IEnumerable<Category> GetAll();

        // Get all post linked to this Category
        IEnumerable<dynamic> GetPosts(int id);

        bool Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExampleApi.Interface
{
    public interface IPostOperation
    {
        IEnumerable<Post> GetAll();

        Post Get(int id);

        bool AddPost(Post post);

        bool Delete(int id);

        bool Update(Post post);
    }
}

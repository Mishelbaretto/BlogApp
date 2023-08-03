using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDBContext _bloggieDBContext;

        public BlogPostRepository(BloggieDBContext bloggieDBContext)
        {
            _bloggieDBContext = bloggieDBContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieDBContext.AddAsync(blogPost);
            await _bloggieDBContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _bloggieDBContext.BlogPosts.Include(x=>x.Tags).ToListAsync();//for using include, the proprty has to be in domain model
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
          return await  _bloggieDBContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id); 
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}

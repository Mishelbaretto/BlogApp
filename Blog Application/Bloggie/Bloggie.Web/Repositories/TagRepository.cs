using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDBContext _bloggieDBContext;

        public TagRepository(BloggieDBContext bloggieDBContext)
        {
            _bloggieDBContext = bloggieDBContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _bloggieDBContext.Tags.AddAsync(tag);
            await _bloggieDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await _bloggieDBContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                _bloggieDBContext.Remove(existingTag);
                await _bloggieDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            //use dbcontext to read tags
            return await _bloggieDBContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
         return  await _bloggieDBContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _bloggieDBContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _bloggieDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}

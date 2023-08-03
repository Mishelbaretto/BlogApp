using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);//needs an enitre tag object to save the changes in db,so paramter will be tag
        Task<Tag?> UpdateAsync(Tag tag);//it can be null or tag so?
        Task<Tag?> DeleteAsync(Guid id);

    }
}

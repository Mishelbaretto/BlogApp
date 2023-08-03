using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags fromrepository
            var tags = await _tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //map view model to domainmodel
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
                //tags is of type ienumerable, we have selected tag we have to modify
            };
            //Map tags from selected tags
            var selectedTags=new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetByIdAsync(selectedTagIdAsGuid);
                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //mappijng tags back to domain model
            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {//call the repository to get data

          var blogPosts=  await _blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)//make sure the name over here matches the route id that we passed to edit button
        {
            //call the repository to get data
         var blogPost=  await _blogPostRepository.GetByIdAsync(id);

            //pass data to view
            return View();
        }
    }
}

using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            //asynchronous is not required, since nothing is called inside the metod like no db queries or no file access

            return View();//when we dont specify the a name for a view it automatically looks for the name of the action method inside the views folder and the shared folder
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            //// 1st method
            ////var tag= _bloggieDbContext.Tags.Find(id);

            ////2nd method
            //var tag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
             var tag = await _tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                var updateTagRequest = new UpdateTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(updateTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagRequest updateTagRequest)
        {//we have updateTagRequest, but we want to convert it to tag,i;e tag domain model bcs _bloggieDbContext cares about entities not the view models
            var tag = new Tag
            {
                Id = updateTagRequest.Id,
                Name = updateTagRequest.Name,
                DisplayName = updateTagRequest.DisplayName
            };
            var updateTag = await _tagRepository.UpdateAsync(tag);
            if (updateTag != null)
            {
                //show success
            }
            else
            {
                //show error notification

            }
            return RedirectToAction("Update", new { id = updateTagRequest.Id });
        }
        //everything is happening inside a controller like reading a database or riting to a database is happeneing inside the controller, we have to make methods inside the controller as asynchronous


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTagRequest updateTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(updateTagRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
            }

            return RedirectToAction("Update", new { id = updateTagRequest.Id });

        }
    }
}

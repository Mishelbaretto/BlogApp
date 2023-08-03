namespace Bloggie.Web.Models.ViewModels
{
    public class UpdateTagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}

using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public IFormFile featuredImage { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        public IBlogPostRepository _BlogPostRepository { get; }

        public AddModel(IBlogPostRepository blogPostRepository)
        {
            _BlogPostRepository = blogPostRepository;
        }

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPost()
        {
            var NewBlogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible,
                Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim()}) )
            };

            await _BlogPostRepository.AddAsync(NewBlogPost);

            var notification = new Notification
            {
                Message = "Blog Post created successfully!",
                Type = Enums.NotificationType.Success
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}

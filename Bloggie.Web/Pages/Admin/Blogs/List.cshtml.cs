using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        public List<BlogPost> ListBlogPost { get; set; }
        public IBlogPostRepository _BlogPostRepository { get; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
            _BlogPostRepository = blogPostRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];

            if (notificationJson != null) 
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            ListBlogPost = (await _BlogPostRepository.GetAllAsync())?.ToList();
        }
    }
}

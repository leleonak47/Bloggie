using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public IFormFile featuredImage { get; set; }
        public IBlogPostRepository _BlogPostRepository { get; }

        public EditModel(IBlogPostRepository blogPostRepository)
        {
            _BlogPostRepository = blogPostRepository;
        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await _BlogPostRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                await _BlogPostRepository.UpdateAsync(BlogPost);

                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong!",
                    Type = Enums.NotificationType.Error
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _BlogPostRepository.DeleteAsync(BlogPost.Id);
            
            if (deleted)
            {
                var notification = new Notification
                {
                    Message = "Blog Post was deleted successfully!",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}

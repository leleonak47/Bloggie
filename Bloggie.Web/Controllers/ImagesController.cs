using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageurl = await _imageRepository.UploadAsync(file);

            if (imageurl == null)
            {
                return Problem("something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return Json(new { link = imageurl });
        }
    }
}

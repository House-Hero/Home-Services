using Microsoft.AspNetCore.Mvc;
using BLL.Interface; // Assuming your interface is in BLL.Interfaces

public class ImageController : Controller
{
    private readonly ICloudinaryService _cloudinaryService;

    public ImageController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file selected.");
        }

        var imageUrl = await _cloudinaryService.UploadImageAsync(file);
        return Ok(new { imageUrl });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(string publicId)
    {
        var result = await _cloudinaryService.DeleteImageAsync(publicId);
        if (result)
        {
            return Ok("Image deleted successfully.");
        }

        return BadRequest("Failed to delete image.");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateImage(string publicId, IFormFile newFile)
    {
        var updatedImageUrl = await _cloudinaryService.UpdateImageAsync(publicId, newFile);
        return Ok(new { updatedImageUrl });
    }
}

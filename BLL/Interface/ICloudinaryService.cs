using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task<bool> DeleteImageAsync(string publicId);
    Task<string> UpdateImageAsync(string publicId, IFormFile newFile);
}

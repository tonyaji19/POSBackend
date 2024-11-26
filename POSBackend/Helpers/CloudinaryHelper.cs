using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace POSBackend.Helpers
{
    public class CloudinaryHelper
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryHelper(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream())
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult?.SecureUrl != null)
            {
                return uploadResult.SecureUrl.AbsoluteUri;
            }

            throw new Exception("Image upload failed.");
        }
    }
}

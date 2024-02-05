using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ChristianBeauty.Utilities
{
    public static class FileHandler
    {
        public static async Task<string?> ImageUploadAsync(IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            if (formFile.Length > 0)
            {
                var uniqueFileName = GetUniqueFileName(formFile.FileName);
                var uploadFolderPath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    "Images",
                    "ProductImages"
                );

                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }
                var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                ResizeImageAsync(filePath);
                return uniqueFileName;
            }
            return null;
        }

        private static void ResizeImageAsync(string originalImagePath)
        {
            using (var image = Image.Load(originalImagePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(1000, 1000),
                    Mode = ResizeMode.Max 
                }));
                image.Save(originalImagePath);
            }
        }

        public static void DeleteImage(string imageName, IWebHostEnvironment webHostEnvironment)
        {
            var filePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                "Images",
                "ProductImages",
                imageName
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                var folderPath = Path.GetDirectoryName(filePath);
                if (Directory.GetFiles(folderPath).Length == 0)
                {
                    Directory.Delete(folderPath);
                }
            }
        }


        private static string GetUniqueFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"{name}_{timestamp}{extension}";
        }
    }
}

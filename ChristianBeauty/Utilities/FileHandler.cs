namespace ChristianBeauty.Utilities
{
    public static class FileHandler
    {
        public static async Task<string?> ImageUploadAsync(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                var uniqueFileName = GetUniqueFileName(formFile.FileName);
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Images/ProductImages",
                    uniqueFileName
                );

                using var stream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(stream);

                return uniqueFileName;
            }
            return null;
        }

        public static void DeleteImage(string imageName)
        {
            var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Images/ProductImages",
                    imageName
                );
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
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

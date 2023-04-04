using BPJ_U2T.Interfaces;

namespace BPJ_U2T.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public UploadFileService(IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;   //เข้าหา wwwroot
            this.configuration = configuration;             //เข้าหา appsettings.json  
        }

        public Task DeleteImages(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var uploadPath = $"{webHostEnvironment.WebRootPath}/images/";
                string fullName = uploadPath + fileName;

                if (File.Exists(fullName)) File.Delete(fullName);
            }
            return Task.CompletedTask;
        }

        public Task DeleteVedio(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var uploadPath = $"{webHostEnvironment.WebRootPath}/vedio/";
                string fullName = uploadPath + fileName;

                if (File.Exists(fullName)) File.Delete(fullName);
            }
            return Task.CompletedTask;
        }

        public bool IsUpload(IFormFileCollection formFiles)
        {
            return formFiles != null || formFiles?.Count > 0;

            //if(formFiles != null){var test = formFiles.Count() > 
        }

        public async Task<List<string>> UploadImages(IFormFileCollection formFiles)
        {
            var listFileName = new List<string>();
            var uploadPath = $"{webHostEnvironment.WebRootPath}/images/";

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var formFile in formFiles)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = uploadPath + fileName;

                using (var stream = File.Create(fullName))
                {
                    await formFile.CopyToAsync(stream);
                }
                listFileName.Add(fileName);
            }
            return listFileName;

        }

        public async Task<List<string>> UploadVedio(IFormFileCollection formFiles)
        {
            var listFileName = new List<string>();
            // uploadPath จะเอามาบวกกับชื่อไฟล์
            var uploadPath = $"{webHostEnvironment.WebRootPath}/vedio/";

            // ถ้ามันไม่มีไฟล์น้ให้สร้างขึ้นมา
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            foreach (var formFile in formFiles)
            {
                // Guid.NewGuid().ToString() สุ่ม id ขึ้นมา + Path.GetExtension(formFile.FileName) เอานามสกุลมา Ex 111111111111.jpg
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = uploadPath + fileName;
                // สร้่างในมันมีตัวตน
                using (var stream = File.Create(fullName))
                {
                    // Copy เนื้อ ไฟล์มา
                    await formFile.CopyToAsync(stream);
                }
                // นำชื่อไฟล์ใส่ใน List
                listFileName.Add(fileName);
            }
            return listFileName;
        }

        public string Validation(IFormFileCollection formFiles)
        {
            foreach (var file in formFiles)
            {
                if (!ValidationExtension(file.FileName))
                {
                    return "Invalid file extension";
                }

                if (!ValidationSize(file.Length))
                {
                    return "The file is too large";
                }
            }
            return null;
        }

        public bool ValidationExtension(string filename)
        {
            string[] permittedExtensions = { ".jpg", ".png", ".mov", ".mp4" };
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                return false;
            };

            return true;
        }
        public bool ValidationSize(long fileSize) => configuration.GetValue<long>("FileSizeLimit") > fileSize;
    }
}

namespace WebAPI.Services.BusinessLogic.CloudStorage
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using WebAPI.DTOs.Storage;
    using WebAPI.Models;

    public class CloudinaryService : ICloudStorageService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(CloudinaryCofigKeys cofigKeys)
        {
            Account account = new Account(cofigKeys.CloudName, cofigKeys.ApiKey, cofigKeys.ApiSecret);

            this.cloudinary = new Cloudinary(account);
            this.cloudinary.Api.Secure = true;
        }

        public async Task<RequestResultDTO<string>> SaveImageAsync(Stream file, string folderName)
        {
            if (file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();

                string path = $"{folderName}/{fileName}";

                try
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        UseFilenameAsDisplayName = true,
                        File = new FileDescription(fileName, file),
                        PublicId = path,
                        Overwrite = true,
                    };

                    var result = await this.cloudinary.UploadAsync(uploadParams);
                    if (result.Error != null)
                    {
                        return new RequestResultDTO<string>
                        {
                            IsSuccessful = false,
                            Message = result.Error.Message,
                        };
                    }

                    return new RequestResultDTO<string>
                    {
                        Data = result.SecureUrl.ToString(),
                        IsSuccessful = true,
                    };
                }
                catch (Exception e)
                {
                    return new RequestResultDTO<string>
                    {
                        IsSuccessful = false,
                        Message = e.Message,
                    };
                }
            }

            return new RequestResultDTO<string>
            {
                IsSuccessful = false,
                Message = "File size was 0!",
            };
        }
    }
}

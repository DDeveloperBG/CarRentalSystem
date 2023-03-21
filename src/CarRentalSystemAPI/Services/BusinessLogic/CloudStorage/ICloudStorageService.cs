namespace WebAPI.Services.BusinessLogic.CloudStorage
{
    using WebAPI.Models;

    public interface ICloudStorageService
    {
        Task<RequestResultDTO<string>> SaveImageAsync(Stream qualificationDoc, string folderName);
    }
}

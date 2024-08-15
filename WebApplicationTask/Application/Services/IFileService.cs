namespace WebApplicationTask.Application.Services;

public interface IFileService
{
    string UploadImage(IFormFile file);
    void DeleteImage(string fileName);
}

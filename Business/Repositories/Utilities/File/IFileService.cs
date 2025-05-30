using Microsoft.AspNetCore.Http;

namespace Business.Repositories.Utilities.File
{
    public interface IFileService
    {
        string FileSaveToServer(IFormFile file, string filePath);
        string FileSaveToFtp(IFormFile file );
        byte[] FileConvertByteArrayToDatabase(IFormFile file);
    }
} 
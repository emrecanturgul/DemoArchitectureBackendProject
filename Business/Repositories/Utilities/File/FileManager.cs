using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using Business.Repositories.Utilities.File;

namespace Business.Concrete 
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower(); 
            string fileName = Guid.NewGuid().ToString() + fileFormat; 
            string path = filePath + fileName; 
            using(var stream = System.IO.File.Create(path)) {
                    file.CopyTo(stream);
            }
            return fileName; 
        }

        public string FileSaveToFtp(IFormFile file)
        {  var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower(); 
            string fileName = Guid.NewGuid().ToString() + fileFormat; 
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp adresi" +fileName);
            request.Credentials = new NetworkCredential("kullanıcı adı","şifre"); 
            request.Method = WebRequestMethods.Ftp.UploadFile; 
            using(Stream ftpStream = request.GetRequestStream()) {
                file.CopyTo(ftpStream); 
            }
            return fileName; 
        }
        

        public byte[] FileConvertByteArrayToDatabase(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }


    }
}
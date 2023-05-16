using CourseProject.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Infrastructure;

public class FileService : IFileService
{
    private const string UPLOAD_DIRECTORY = "uploads";

    public FileService()
    {
        if (!Directory.Exists(UPLOAD_DIRECTORY))
            Directory.CreateDirectory(UPLOAD_DIRECTORY);

    }
    public void DeleteFile(string filePath)
    {
        var path = Path.Combine(UPLOAD_DIRECTORY, filePath);
        File.Delete(path);
    }

    public byte[] GetFile(string filePath)
    {
        var path = Path.Combine(UPLOAD_DIRECTORY, filePath);
        return File.ReadAllBytes(path);
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
        var path = Path.Combine(UPLOAD_DIRECTORY, uniqueFilename);
        await file.CopyToAsync(new FileStream(path, FileMode.Create));
        return uniqueFilename;
    }
}

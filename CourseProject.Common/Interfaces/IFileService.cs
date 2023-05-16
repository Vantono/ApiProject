using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Interfaces;

public interface IFileService
{
    void DeleteFile(string filePath);
    Task<string> SaveFileAsync(IFormFile filePath);
    byte[] GetFile(string filePath);

}

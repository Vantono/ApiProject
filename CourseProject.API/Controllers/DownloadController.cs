using CourseProject.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DownloadController : ControllerBase
{
    private IFileService FileService { get; }

    public DownloadController(IFileService fileService)
    {
       FileService = fileService;
    }


    [HttpGet]
    [Route("Get/{path}")]
    public async Task<IActionResult> GetFile(string path)
    {
        var bytes = FileService.GetFile(path);
        return File(bytes, "APPLICATION/octet-stream", path);
    }

}


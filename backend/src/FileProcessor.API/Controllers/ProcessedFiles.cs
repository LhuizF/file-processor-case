using System.Threading.Tasks;
using FileProcessor.Domain.Exceptions;
using FileProcessor.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProcessedFiles : ControllerBase
  {
    private readonly IProcessedFileService _processedFileService;
    public ProcessedFiles(IProcessedFileService processedFileService)
    {
      _processedFileService = processedFileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProcessedFiles()
    {
      try
      {
        var processedFiles = await _processedFileService.GetProcessedFilesAsync();
        return Ok(processedFiles);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro interno: {ex.Message}");
      }
    }

  }
}

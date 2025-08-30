using FileProcessor.Domain.Exceptions;
using FileProcessor.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AcquirerFiles : ControllerBase
  {
    private readonly IAcquirerFileService _acquirerFileService;
    public AcquirerFiles(IAcquirerFileService acquirerFileService)
    {
      _acquirerFileService = acquirerFileService;
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
      if (file == null)
      {
        return BadRequest(new { message = "Nenhum arquivo foi enviado." });
      }
      try
        {
            _acquirerFileService.AddToProcessing(file.FileName, file.Length);

            return Accepted(new { message = "Arquivo validado e recebido para processamento." });
        }
        catch (FileValidationException ex)
        {
          return BadRequest(new { message = ex.Message });
        }
        catch
        {
          return StatusCode(500, new { message = "Ocorreu um erro interno no servidor." });
        }
    }

  }
}

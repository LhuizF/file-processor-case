using FileProcessor.Domain.Interface;
using FileProcessor.Domain.Exceptions;
using FileProcessor.Application.Contracts;
using System.Threading.Tasks;
using FileProcessor.Domain.Dtos;

namespace FileProcessor.Application.Services;

public class AcquirerFileService : IAcquirerFileService
{
  private readonly long _maxFileSize = 10 * 1024 * 1024;

  private readonly IFileStore _fileStore;
  private readonly IBackgroundTaskQueue _taskQueue;

  public AcquirerFileService(IFileStore fileStore, IBackgroundTaskQueue taskQueue)
  {
    _fileStore = fileStore;
    _taskQueue = taskQueue;
  }

  public async Task AddToProcessing(string fileName, long fileSize, Stream fileStream)
  {

    if (fileSize == 0)
    {
      throw new FileValidationException("Arquivo vazio");
    }

    if (fileSize > _maxFileSize)
    {
      throw new FileValidationException($"O tamanho máximo de arquivo permitido é {_maxFileSize / (1024 * 1024)} MB.");
    }

    if (Path.GetExtension(fileName).ToLowerInvariant() != ".txt")
    {
      throw new FileValidationException("Formato de arquivo inválido. Apenas arquivos .txt são permitidos.");
    }

    var path = await _fileStore.SaveFileAsync(fileName, fileStream);

    await _taskQueue.Publish(new ProcessFileMessage()
    {
      Filename = fileName,
      Path = path,
      ReceivedAt = DateTime.UtcNow,
    });

  }
}

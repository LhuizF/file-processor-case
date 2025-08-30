using FileProcessor.Domain.Interface;
using FileProcessor.Domain.Exceptions;

namespace FileProcessor.Application.Services;

public class AcquirerFileService : IAcquirerFileService
{
  private readonly long _maxFileSize = 10 * 1024 * 1024;

  public void AddToProcessing(string fileName, long fileSize)
  {

    if (fileSize == 0)
    {
      throw new FileValidationException("Arquivo vazio");
    }
    Console.WriteLine(fileSize);
    Console.WriteLine(fileSize > _maxFileSize);
    if (fileSize > _maxFileSize)
    {
      throw new FileValidationException($"O tamanho máximo de arquivo permitido é {_maxFileSize / (1024 * 1024)} MB.");
    }

    if (Path.GetExtension(fileName).ToLowerInvariant() != ".txt")
    {
      throw new FileValidationException("Formato de arquivo inválido. Apenas arquivos .txt são permitidos.");
    }
    //TODO: adicionar a fila
  }
}

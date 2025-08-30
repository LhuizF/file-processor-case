using FileProcessor.Domain.Dtos;
using FileProcessor.Domain.Interface;

namespace FileProcessor.Application.Services;

public class ProcessAcquirerFileService : IProcessAcquirerFileService
{
  public Task ProcessFileAsync(ProcessFileMessage processFileMessage)
  {
    Console.WriteLine($"Processando arquivo: {processFileMessage.Filename}");
    return Task.CompletedTask;
  }
}

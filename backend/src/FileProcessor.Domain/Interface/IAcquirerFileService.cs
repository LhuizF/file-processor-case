namespace FileProcessor.Domain.Interface;

public interface IAcquirerFileService
{
  Task AddToProcessing(string fileName, long fileSize, Stream fileStream);
}


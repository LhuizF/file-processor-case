namespace FileProcessor.Domain.Interface;

public interface IAcquirerFileService
{
  void AddToProcessing(string fileName, long fileSize);
}


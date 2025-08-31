using FileProcessor.Domain.Dtos;

namespace FileProcessor.Domain.Interface;

public interface IProcessedFileService
{
  Task<List<ProcessedFileDto>> GetProcessedFilesAsync();
}


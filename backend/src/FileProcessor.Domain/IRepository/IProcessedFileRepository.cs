using FileProcessor.Domain.Entities;

namespace FileProcessor.Domain.IRepository;
public interface IProcessedFileRepository
{
  Task AddAsync(ProcessedFile processedFile);
}


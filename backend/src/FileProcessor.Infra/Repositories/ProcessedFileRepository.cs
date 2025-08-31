using FileProcessor.Domain.Entities;
using FileProcessor.Domain.IRepository;
using FileProcessor.Infra.Context;

namespace FileProcessor.Infra.Repositories;
public class ProcessedFileRepository : IProcessedFileRepository
{
  private readonly FileProcessorDbContext _context;

  public ProcessedFileRepository(FileProcessorDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(ProcessedFile processedFile)
  {
    await _context.ProcessedFiles.AddAsync(processedFile);
    await _context.SaveChangesAsync();
  }
}

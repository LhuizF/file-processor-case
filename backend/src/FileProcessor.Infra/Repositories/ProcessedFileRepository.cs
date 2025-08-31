using FileProcessor.Domain.Entities;
using FileProcessor.Domain.IRepository;
using FileProcessor.Infra.Context;
using Microsoft.EntityFrameworkCore;

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

  public async Task<List<ProcessedFile>> GetAllProcessedFilesAsync()
  {
    return await _context.ProcessedFiles.ToListAsync();
  }
  public (int SuccessCount, int FailureCount) CountProcessedFilesByStatusAsync()
  {
    var successCount = _context.ProcessedFiles.Count(pf => pf.Status == FileStatus.Success);
    var failureCount = _context.ProcessedFiles.Count(pf => pf.Status == FileStatus.Failure);
    return (successCount, failureCount);
  }
}

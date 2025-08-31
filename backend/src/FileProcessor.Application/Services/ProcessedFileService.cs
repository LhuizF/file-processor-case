using FileProcessor.Domain.Interface;
using FileProcessor.Domain.Dtos;
using FileProcessor.Domain.IRepository;

namespace FileProcessor.Application.Services;

public class ProcessedFileService : IProcessedFileService
{
  private readonly IProcessedFileRepository _processedFileRepository;

  public ProcessedFileService(IProcessedFileRepository processedFileRepository)
  {
    _processedFileRepository = processedFileRepository;
  }

  public async Task<List<ProcessedFileDto>> GetProcessedFilesAsync()
  {
    var processedFiles = await _processedFileRepository.GetAllProcessedFilesAsync();

    return processedFiles.Select(file => new ProcessedFileDto
    {
      Id = file.Id,
      FileName = file.FileName,
      Status = file.Status,
      AcquirerName = file.AcquirerName,
      ProcessedAt = file.ProcessedAt,
      EstablishmentCode = file.EstablishmentCode,
      FilePath = file.FilePath,
      PeriodStartDate = file.PeriodStartDate,
      PeriodEndDate = file.PeriodEndDate,
      ProcessingDate = file.ProcessingDate,
      SequenceNumber = file.SequenceNumber,
      ErrorMessage = file.ErrorMessage,
    }).ToList();
  }

  public ProcessedFilesStatusDto GetProcessedFilesStatusAsync()
  {
    var (successCount, failureCount) = _processedFileRepository.CountProcessedFilesByStatusAsync();

    var totalFiles = successCount + failureCount;

    return new ProcessedFilesStatusDto
    {
      TotalFiles = totalFiles,
      Success = new Result
      {
        Total = successCount,
        Percent = successCount == 0 ? 0 : Math.Round((successCount / (double)totalFiles) * 100, 2)
      },
      Failure = new Result
      {
        Total = failureCount,
        Percent = failureCount == 0 ? 0 : Math.Round((failureCount / (double)totalFiles) * 100, 2)
      }
    };
  }
}

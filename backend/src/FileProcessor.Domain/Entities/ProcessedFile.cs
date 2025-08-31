namespace FileProcessor.Domain.Entities;
public static class FileStatus
{
  public const string Success = "Recepcionado";
  public const string Failure = "Não Recepcionado";
}

public class ProcessedFile
{
  public Guid Id { get; set; }
  public string FileName { get; set; }
  public string Status { get; set; }
  public string FilePath { get; set; }
  public DateTime ProcessedAt { get; set; }
  public string? AcquirerName { get; set; }
  public string? EstablishmentCode { get; set; }
  public DateTime? ProcessingDate { get; set; }
  public DateTime? PeriodStartDate { get; set; }
  public DateTime? PeriodEndDate { get; set; }
  public int? SequenceNumber { get; set; }
  public string? ErrorMessage { get; set; }
}

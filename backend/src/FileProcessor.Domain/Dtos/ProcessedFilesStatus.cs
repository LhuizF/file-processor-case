namespace FileProcessor.Domain.Dtos;

public class ProcessedFilesStatusDto
{
  public int TotalFiles { get; set; }
  public int Success { get; set; }
  public int Failure { get; set; }
}

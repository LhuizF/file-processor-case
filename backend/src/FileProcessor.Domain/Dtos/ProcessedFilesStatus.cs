namespace FileProcessor.Domain.Dtos;

public class ProcessedFilesStatusDto
{
  public int TotalFiles { get; set; }
  public Result Success { get; set; } = new Result();
  public Result Failure { get; set; } = new Result();
}
public class Result
{
  public int Total { get; set; }
  public double Percent { get; set; }
}

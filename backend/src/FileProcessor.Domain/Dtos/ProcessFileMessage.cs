namespace FileProcessor.Domain.Dtos;
public class ProcessFileMessage
{
  public required string Filename { get; set; }
  public required string Path { get; set; }
  public required DateTime ReceivedAt { get; set; }
}

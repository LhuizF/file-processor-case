namespace FileProcessor.Domain.Dtos;
public class ProcessFileMessage
{
  public Guid Id { get; set; }
  public required string Filename { get; set; }
  public required string Path { get; set; }
  public required DateTime ReceivedAt { get; set; }
}

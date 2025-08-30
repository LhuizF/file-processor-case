namespace FileProcessor.Domain.Interface;
public interface IFileStore
{
  Task<string> SaveFileAsync(string originalFileName, Stream fileStream);

  void DeleteFile(string filePath);
}

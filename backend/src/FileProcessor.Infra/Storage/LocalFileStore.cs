using FileProcessor.Domain.Interface;
using Microsoft.Extensions.Hosting;

namespace FileProcessor.Infra.Storage;
public class LocalFileStore : IFileStore
{
  private readonly string _storagePath;

  public LocalFileStore(IHostEnvironment env)
  {
    _storagePath = Path.Combine(env.ContentRootPath, "temp");
    if (!Directory.Exists(_storagePath))
    {
        Directory.CreateDirectory(_storagePath);
    }
  }

  public async Task<string> SaveFileAsync(string originalFileName, Stream fileStream)
  {
    var fullPath = Path.Combine(_storagePath, originalFileName);

    using var output = new FileStream(fullPath, FileMode.Create);
    await fileStream.CopyToAsync(output);

    return "/" + fullPath.Replace("\\", "/");
  }

  public void DeleteFile(string filePath)
  {
    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }
  }
}

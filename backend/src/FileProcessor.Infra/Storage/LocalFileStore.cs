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

  public async Task<string> SaveFileAsync(string fileName, Stream fileStream)
  {
    var filePath = Path.Combine(_storagePath, fileName);

    await using (var file = new FileStream(filePath, FileMode.Create))
    {
        fileStream.Seek(0, SeekOrigin.Begin);
        await fileStream.CopyToAsync(file);
    }

    return filePath;
}

  public void DeleteFile(string filePath)
  {
    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }
  }
}

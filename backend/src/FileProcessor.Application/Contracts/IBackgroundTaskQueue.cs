using FileProcessor.Domain.Dtos;

namespace FileProcessor.Application.Contracts;
public interface IBackgroundTaskQueue
{
  Task Publish(ProcessFileMessage message);
  IAsyncEnumerable<ProcessFileMessage> ReadAllAsync(CancellationToken cancellationToken);
}

using System.Threading.Channels;
using FileProcessor.Application.Contracts;
using FileProcessor.Domain.Dtos;

namespace FileProcessor.Infra.Messaging;
public class InMemoryBackgroundTaskQueue : IBackgroundTaskQueue
{
  private readonly Channel<ProcessFileMessage> _queue = Channel.CreateUnbounded<ProcessFileMessage>();

  public async Task Publish(ProcessFileMessage message)
  {
    if (message is null)
    {
      throw new ArgumentNullException(nameof(message));
    }
    await _queue.Writer.WriteAsync(message);
  }

  public IAsyncEnumerable<ProcessFileMessage> ReadAllAsync(CancellationToken cancellationToken)
  {
      return _queue.Reader.ReadAllAsync(cancellationToken);
  }
}


using FileProcessor.Application.Contracts;
using FileProcessor.Domain.Dtos;
using FileProcessor.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileProcessor.Infra.Consumer;
public class FileProcessingConsumer : BackgroundService
{
  private readonly IBackgroundTaskQueue _queue;
  private readonly IServiceProvider _serviceProvider;

  public FileProcessingConsumer(IBackgroundTaskQueue queue, IServiceProvider serviceProvider)
  {
    _queue = queue;
    _serviceProvider = serviceProvider;
    Console.WriteLine("FileProcessingConsumer init");
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {

    await foreach (var message in _queue.ReadAllAsync(stoppingToken))
    {
        await ProcessMessageAsync(message, stoppingToken);
    }
  }

  private async Task ProcessMessageAsync(ProcessFileMessage message, CancellationToken stoppingToken)
  {
    try
    {

      using var scope = _serviceProvider.CreateScope();

      var fileProcessor = scope.ServiceProvider.GetRequiredService<IProcessAcquirerFileService>();

      await fileProcessor.ProcessFileAsync(message);
    }
    catch (Exception ex)
    {
      // TODO: implementar retry ou requeue
      Console.WriteLine($"Error processing message: {ex.Message}");
    }
  }
}

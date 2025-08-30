using FileProcessor.Domain.Dtos;

namespace FileProcessor.Domain.Interface;

public interface IProcessAcquirerFileService
{
    Task ProcessFileAsync(ProcessFileMessage processFileMessage);
}

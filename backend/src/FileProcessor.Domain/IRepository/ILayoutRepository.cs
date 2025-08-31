using FileProcessor.Domain.Entities;

namespace FileProcessor.Domain.IRepository;
public interface ILayoutRepository
{
    Task<Layout> GetLayoutByIdentifierAsync(char recordIdentifier);
}

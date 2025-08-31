using FileProcessor.Domain.Entities;

public interface ILayoutRepository
{
    Task<Layout> GetLayoutByIdentifierAsync(char recordIdentifier);
}

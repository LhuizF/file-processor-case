using FileProcessor.Domain.Entities;
using FileProcessor.Domain.IRepository;
using FileProcessor.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Infra.Repositories;
public class LayoutRepository : ILayoutRepository
{
  private readonly FileProcessorDbContext _context;

  public LayoutRepository(FileProcessorDbContext context)
  {
    _context = context;
  }

  public async Task<Layout?> GetLayoutByIdentifierAsync(char identifier)
  {
    return await _context.Layouts
      .Include(l => l.Fields)
      .FirstOrDefaultAsync(l => l.Identifier == identifier);
  }

}

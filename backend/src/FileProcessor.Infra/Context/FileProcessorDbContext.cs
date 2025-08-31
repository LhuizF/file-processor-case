using FileProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Infra.Context;
public class FileProcessorDbContext : DbContext
{
  public FileProcessorDbContext(DbContextOptions<FileProcessorDbContext> options) : base(options)
  {
  }

  public DbSet<Layout> Layouts { get; set; }
  public DbSet<LayoutField> LayoutFields { get; set; }
  public DbSet<ProcessedFile> ProcessedFiles { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Layout>().HasKey(l => l.Id);
    modelBuilder.Entity<LayoutField>().HasKey(f => f.Id);
    modelBuilder.Entity<ProcessedFile>().HasKey(p => p.Id);

    modelBuilder.Entity<Layout>()
      .HasMany(l => l.Fields)
      .WithOne(f => f.Layout)
      .HasForeignKey(f => f.LayoutId);

    var ufCardLayoutId = new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b");
    var fagammonCardLayoutId = new Guid("8a6b1e3c-1b9d-4e2a-9f8c-3d4a5b6c7d8e");


    modelBuilder.Entity<Layout>().HasData(
      new Layout { Id = ufCardLayoutId, AcquirerName = "UfCard", Identifier = '0' },
      new Layout { Id = fagammonCardLayoutId, AcquirerName = "FagammonCard", Identifier = '1' }
    );

    modelBuilder.Entity<LayoutField>().HasData(
      new LayoutField
      {
        Id = new Guid("c1b2a3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"),
        LayoutId = ufCardLayoutId,
        Name = "Estabelecimento",
        InitPosition = 2,
        Length = 10,
        DataType = "NUM"
      },
      new LayoutField
      {
        Id = new Guid("d2c3b4e5-f6a7-b8c9-d0e1-f2a3b4c5d6e7"),
        LayoutId = ufCardLayoutId,
        Name = "DataProcessamento",
        InitPosition = 12,
        Length = 8,
        DataType = "DATE_YYYYMMDD"
      },
      new LayoutField
      {
        Id = new Guid("e3d4c5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8"),
        LayoutId = ufCardLayoutId,
        Name = "PeriodoInicial",
        InitPosition = 20,
        Length = 8,
        DataType = "DATE_YYYYMMDD"
      },
      new LayoutField
      {
        Id = new Guid("f4e5d6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9"),
        LayoutId = ufCardLayoutId,
        Name = "PeriodoFinal",
        InitPosition = 28,
        Length = 8,
        DataType = "DATE_YYYYMMDD"
      },
      new LayoutField
      {
        Id = new Guid("a5f6e7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"),
        LayoutId = ufCardLayoutId,
        Name = "Sequencia",
        InitPosition = 36,
        Length = 7,
        DataType = "NUM"
      },

      new LayoutField
      {
        Id = new Guid("b6a7f8c9-d0e1-f2a3-b4c5-d6e7f8a9b0c1"),
        LayoutId = fagammonCardLayoutId,
        Name = "DataProcessamento",
        InitPosition = 2,
        Length = 8,
        DataType = "DATE_YYYYMMDD"
      },
      new LayoutField
      {
        Id = new Guid("c7b8a9d0-e1f2-a3b4-c5d6-e7f8a9b0c1d2"),
        LayoutId = fagammonCardLayoutId,
        Name = "Estabelecimento",
        InitPosition = 10,
        Length = 8,
        DataType = "NUM"
      },
      new LayoutField
      {
        Id = new Guid("d8c9b0e1-f2a3-b4c5-d6e7-f8a9b0c1d2e3"),
        LayoutId = fagammonCardLayoutId,
        Name = "Sequencia",
        InitPosition = 30,
        Length = 7,
        DataType = "NUM"
      }
    );

  }
}

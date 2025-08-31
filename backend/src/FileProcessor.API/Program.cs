using FileProcessor.Application.Contracts;
using FileProcessor.Application.Services;
using FileProcessor.Domain.Interface;
using FileProcessor.Infra.Consumer;
using FileProcessor.Infra.Context;
using FileProcessor.Infra.Storage;
using FileProcessor.Infra.Messaging;
using Microsoft.EntityFrameworkCore;
using FileProcessor.Infra.Repositories;
using FileProcessor.Domain.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<FileProcessorDbContext>(options => options.UseNpgsql(connectionString));

//Dependency Injection
builder.Services.AddScoped<IAcquirerFileService, AcquirerFileService>();
builder.Services.AddScoped<IFileStore, LocalFileStore>();
builder.Services.AddScoped<IProcessAcquirerFileService, ProcessAcquirerFileService>();
builder.Services.AddScoped<IProcessedFileService, ProcessedFileService>();

builder.Services.AddSingleton<IBackgroundTaskQueue, InMemoryBackgroundTaskQueue>();
// Repositories
builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
builder.Services.AddScoped<IProcessedFileRepository, ProcessedFileRepository>();

builder.Services.AddHostedService<FileProcessingConsumer>();

var app = builder.Build();

try
{
  using (var scope = app.Services.CreateScope())
  {
    var dbContext = scope.ServiceProvider.GetRequiredService<FileProcessorDbContext>();
    dbContext.Database.Migrate();
  }
}
catch (Exception ex)
{
  Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok());

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

using FileProcessor.Application.Contracts;
using FileProcessor.Application.Services;
using FileProcessor.Domain.Interface;
using FileProcessor.Infra.Consumer;
using FileProcessor.Infra.Storage;
using FileProcessor.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddScoped<IAcquirerFileService, AcquirerFileService>();
builder.Services.AddScoped<IFileStore, LocalFileStore>();
builder.Services.AddScoped<IProcessAcquirerFileService, ProcessAcquirerFileService>();

builder.Services.AddSingleton<IBackgroundTaskQueue, InMemoryBackgroundTaskQueue>();

builder.Services.AddHostedService<FileProcessingConsumer>();

var app = builder.Build();

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

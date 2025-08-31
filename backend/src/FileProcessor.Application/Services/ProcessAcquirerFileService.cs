using FileProcessor.Domain.Dtos;
using FileProcessor.Domain.Entities;
using FileProcessor.Domain.Interface;
using FileProcessor.Domain.IRepository;

namespace FileProcessor.Application.Services;
  public class ProcessAcquirerFileService : IProcessAcquirerFileService
  {
    private readonly ILayoutRepository _layoutRepository;
    private readonly IProcessedFileRepository _processedFileRepository;

    public ProcessAcquirerFileService(ILayoutRepository layoutRepository, IProcessedFileRepository processedFileRepository)
    {
        _layoutRepository = layoutRepository;
        _processedFileRepository = processedFileRepository;
    }

    public async Task ProcessFileAsync(ProcessFileMessage processFileMessage)
    {
      var processedFile = new ProcessedFile
      {
        Id = processFileMessage.Id,
        FileName = processFileMessage.Filename,
        ProcessedAt = DateTime.UtcNow,
        FilePath = processFileMessage.Path
      };

      Console.WriteLine("PATH");
      Console.WriteLine(processFileMessage.Path);

      try
    {
      var line = await File.ReadLinesAsync(processFileMessage.Path).FirstAsync();

      if (string.IsNullOrEmpty(line))
        throw new Exception("O arquivo está vazio e não pode ser processado.");

      var recordIdentifier = line[0];
      var layout = await _layoutRepository.GetLayoutByIdentifierAsync(recordIdentifier);

      if (layout == null)
        throw new Exception($"Layout não encontrado para o identificador: {recordIdentifier}");

      var parsedData = ParseAndValidate(line, layout);

      processedFile.Status = FileStatus.Success;
      MapParsedDataToEntity(processedFile, parsedData);

      // Console.WriteLine($"Arquivo '{processFileMessage.Filename}' processado com SUCESSO.");
    }
    catch (Exception ex)
    {
      // Console.WriteLine($"FALHA ao processar o arquivo '{processFileMessage.Filename}': {ex.processFileMessage}");
      processedFile.Status = FileStatus.Failure;
      processedFile.ErrorMessage = ex.Message;
    }
    finally
    {
      await _processedFileRepository.AddAsync(processedFile);
      // Console.WriteLine($"Registro de processamento para '{message.Filename}' salvo com status '{processedFile.Status}'.");
    }
    }

    private static Dictionary<string, object> ParseAndValidate(string line, Layout layout)
    {
      if (string.IsNullOrWhiteSpace(line) || layout?.Fields == null || !layout.Fields.Any())
        throw new Exception("Layout inválido ou linha de dados vazia.");

      var parsedData = new Dictionary<string, object>
      {
        ["AcquirerName"] = layout.AcquirerName
      };

      foreach (var field in layout.Fields)
      {
        if (line.Length < (field.InitPosition - 1 + field.Length))
          throw new Exception($"A linha é muito curta para extrair o campo '{field.Name}'.");

        string rawValue = line.Substring(field.InitPosition - 1, field.Length).Trim();

        if (!TryConvert(rawValue, field.DataType, out object typedValue))
          throw new Exception($"Erro de conversão no campo '{field.Name}' Valor: '{rawValue}', Tipo esperado: '{field.DataType}'");

        parsedData[field.Name] = typedValue;
      }

      return parsedData;
    }

    private static bool TryConvert(string rawValue, string? dataType, out object result)
    {
      result = rawValue;

      if (dataType == "NUM")
      {
        if (long.TryParse(rawValue, out var n))
        {
          result = n;
          return true;
        }
        return false;
      }

      if (dataType == "DATE_YYYYMMDD")
      {
        if (DateTime.TryParseExact(rawValue, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var d))
        {
          result = DateTime.SpecifyKind(d, DateTimeKind.Utc);
          return true;
        }
        return false;
      }
      return true;
    }

    private void MapParsedDataToEntity(ProcessedFile processedFile, Dictionary<string, object> parsedData)
    {
      processedFile.AcquirerName = parsedData.GetValueOrDefault("AcquirerName")?.ToString();
      processedFile.EstablishmentCode = parsedData.GetValueOrDefault("Estabelecimento")?.ToString();
      processedFile.SequenceNumber = parsedData.ContainsKey("Sequencia") ? Convert.ToInt32(parsedData["Sequencia"]) : null;
      processedFile.ProcessingDate = parsedData.ContainsKey("DataProcessamento") ? (DateTime?)parsedData["DataProcessamento"] : null;
      processedFile.PeriodStartDate = parsedData.ContainsKey("PeriodoInicial") ? (DateTime?)parsedData["PeriodoInicial"] : null;
      processedFile.PeriodEndDate = parsedData.ContainsKey("PeriodoFinal") ? (DateTime?)parsedData["PeriodoFinal"] : null;
    }
}

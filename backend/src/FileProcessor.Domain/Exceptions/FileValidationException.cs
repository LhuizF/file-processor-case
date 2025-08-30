namespace FileProcessor.Domain.Exceptions;

public class FileValidationException(string message) : Exception(message)
{

}

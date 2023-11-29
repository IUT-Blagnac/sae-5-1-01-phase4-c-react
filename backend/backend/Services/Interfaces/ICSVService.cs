namespace backend.Services.Interfaces;

public interface ICSVService
{
    public IEnumerable<T> ReadCSV<T>(Stream file);
}
namespace Froom.Package.Interfaces.Async;

public interface IMapFileAsync<T> where T : class, new()
{
    ILoopAsync<T> MapCsvLineByLineAsync(Action<T, string> action);
    ILoopAsync<T>? MapAsync();
}
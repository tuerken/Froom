namespace Froom.Package.Interfaces.Async;

public interface ILoadFileAsync<T> where T : class, new()
{
    IMapFileAsync<T> FileAsync(string path);
    IMapFileAsync<T> TextAsync(string text);
}
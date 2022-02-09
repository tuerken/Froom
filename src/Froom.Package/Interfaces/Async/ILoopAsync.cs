namespace Froom.Package.Interfaces.Async;

public interface ILoopAsync<T> where T : class, new()
{
    IAsyncEnumerable<T> ToListAsync();
}
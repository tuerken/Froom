using Froom.Package.Definitions;
using Froom.Package.Exceptions;
using Froom.Package.Interfaces.Async;

namespace Froom.Package._Async;

public class FroomFluidAsync<T> : Fluid<T>, ILoadFileAsync<T>, IMapFileAsync<T>, ILoopAsync<T> where T : class, new()
{
    internal FroomFluidAsync()
    {
        Queue = new List<QueueItemType>();
    }

    private List<QueueItemType> Queue { get; }
    private string? Path { get; set; }
    private Action<T, string>? Mapper { get; set; }

    public IMapFileAsync<T> FileAsync(string path)
    {
        if (!File.Exists(path)) throw new FileException($"{path} doesn't exist");
        Queue.Add(QueueItemType.File);
        Path = path;
        return this;
    }

    public IMapFileAsync<T> TextAsync(string text)
    {
        Queue.Add(QueueItemType.Text);
        Data = text;
        return this;
    }

    public async IAsyncEnumerable<T> ToListAsync()
    {
        if (!Queue.Any()) yield break;
        if (!File.Exists(Path)) throw new FileException($"{Path} doesn't exist");
        
        var lines = await File.ReadAllLinesAsync(Path);
        foreach (var line in lines)
        {
            var item = new T();
            Mapper?.Invoke(item, line);
       
            yield return item;
        }
    }

    public ILoopAsync<T> MapCsvLineByLineAsync(Action<T, string> action)
    {
        Queue.Add(QueueItemType.MapWithAction);
        Mapper = action;
        return this;
    }

    public ILoopAsync<T>? MapAsync()
    {
        Queue.Add(QueueItemType.Map);
        return this;
    }
}
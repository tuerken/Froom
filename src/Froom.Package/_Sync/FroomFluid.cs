using System.Collections;
using System.Text.Json;
using Froom.Package.Exceptions;
using Froom.Package.Interfaces.Sync;

namespace Froom.Package._Sync;

public sealed class FroomFluid<T> : Fluid<T>, ILoadFile<T>, IMapFile<T>, ILoop<T> where T : IList, new()
{
    private T? Items { get; set; }
    
    internal FroomFluid()
    {
    }

    public IMapFile<T> File(string path)
    {
        if (!System.IO.File.Exists(path)) throw new FileException($"{path} doesn't exist");
        Data = System.IO.File.ReadAllText(path);
        return this;
    }


    public IMapFile<T> Text(string? text)
    {
        if (text is null) return this;

        Data = text;
        return this;
    }

    public T? ToList()
    {
        if (Data is null) return default;

        return Items;
    }

    public ILoop<T> Map(Action<T, string> action)
    {
        if (Data is null) return this;

        Items = new T();

        action.Invoke(Items, Data);

        return this;
    }

    public ILoop<T> Map()
    {
        if (Data is null) return this;
        // var isJson = Data.Trim().StartsWith("[") || Data.Trim().StartsWith("{");
        Items = JsonSerializer.Deserialize<T>(Data,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return this;
    }
}
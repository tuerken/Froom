using System.Collections;
using System.Text.Json;
using Froom.Package.Exceptions;
using Froom.Package.Interfaces;

namespace Froom.Package;

public sealed class FroomFluid<T> : ILoadFile<T>, IMapFile<T>, ILoop<T> where T : IList, new()
{
    private string? Data { get; set; }
    private T? Object { get; set; }


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

        return Object;
    }

    public ILoop<T>? Map(Action<T?, string?> action)
    {
        if (Data is null) return default;

        Object = new T();

        action.Invoke(Object, Data);

        return this;
    }

    public ILoop<T> Map()
    {
        if (Data is null) return this;
        // var isJson = Data.Trim().StartsWith("[") || Data.Trim().StartsWith("{");
        Object = JsonSerializer.Deserialize<T>(Data,
            options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return this;
    }
}
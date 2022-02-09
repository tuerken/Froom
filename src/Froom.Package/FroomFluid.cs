using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using Froom.Package.Exceptions;
using Froom.Package.Interfaces;

namespace Froom.Package;

public  sealed class FroomFluid<T> : ILoadFile<T>, IMapFile<T>, ILoop<T> where T : IList, new()
{
    private string? Data { get; set; }
    private T? DataObject { get; set; }


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
        Data = text;
        return this;
    }

    public T? ToList()
    {
        return DataObject;
    }

    public ILoop<T> Map(Action<T?, string?> m)
    {
        DataObject = new T();
        m?.Invoke(DataObject, Data);
        return this;
    }

    public ILoop<T> Map()
    {
        DataObject = JsonSerializer.Deserialize<T>(Data, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return this;
    }
}
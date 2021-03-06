using System.Collections;

namespace Froom.Package.Interfaces;

public interface ILoadFile<T> where  T : IList, new()
{
    IMapFile<T> File(string path);
    IMapFile<T> Text(string? text);
}
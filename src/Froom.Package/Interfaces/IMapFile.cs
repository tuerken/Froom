using System.Collections;

namespace Froom.Package.Interfaces;

public interface IMapFile<T> where  T : IList, new()
{
    ILoop<T>? Map(Action<T?, string?> action);
    ILoop<T>? Map();
}
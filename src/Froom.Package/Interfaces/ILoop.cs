using System.Collections;

namespace Froom.Package.Interfaces;

public interface ILoop<T> where T : IList, new()
{
    T? ToList();
}
using System.Collections;

namespace Froom.Package.Interfaces.Sync;

public interface ILoop<T> where T : IList, new()
{
    T? ToList();

    // IAsyncEnumerable<TSingle?> ToListAsync<TSingle>() where TSingle : class , new();
}
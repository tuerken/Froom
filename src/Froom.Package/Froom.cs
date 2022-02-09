using System.Collections;
using Froom.Package._Async;
using Froom.Package._Sync;

namespace Froom.Package;

public static class Froom
{
    public static FroomFluid<T> Load<T>() where T : IList, new()
    {
        return new FroomFluid<T>();
    }
    
    public static FroomFluidAsync<T> LoadAsync<T>() where T : class, new()
    {
        return new FroomFluidAsync<T>();
    }
}
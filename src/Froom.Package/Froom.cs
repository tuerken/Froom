using System.Collections;

namespace Froom.Package;

public static class Froom
{
    public static FroomFluid<T> Load<T>() where T : IList, new()
    {
        return new FroomFluid<T>();
    }
}